﻿using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// 非同期処理を試してみる
    /// </summary>
    public class ParallelTest : IRunnable {

        /// <summary>
        /// ロック用のオブジェクト: こいつを握っている間は他のスレッドはこれを握れず待ち状態にされる
        /// </summary>
        private object sumLock;
        
        /// <summary>
        /// スレッドローカルな変数
        /// </summary>
        private static ThreadLocal<int> threadLocal = new ThreadLocal<int>();

        public void Run() {
            // 単純な関数の並列呼び出し
            // Parallel.Invoke(parallelFuncA, parallelFuncB, parallelFuncC);  // AはOK、Bは引数あるのでNG、Cは戻り値がvoidじゃないのでNG

            // for文の並列版
            Parallel.For(0, 10, i => Debug.WriteLine($"Parallel.For: {i}"));  // 0-9 までの数字が出力される

            // foreach文の並列版
            var rangeFor0to10 = Enumerable.Range(0, 10);
            Parallel.ForEach(rangeFor0to10, i => Debug.WriteLine($"Parallel.ForEach: {i}"));  // 0-9 までの数字が出力される

            // スレッドプールを明示的に使う
            for (int i = 0; i < 10; i++) {
                ThreadPool.QueueUserWorkItem(new WaitCallback(ii => {
                    Debug.WriteLine($"ThreadPool.QueueUserWorkItem [{(int) ii}]: スレッドプールからメソッド実行");
                }), i);
            }

            // lockステートメント
            var sum = 0;
            this.sumLock = new object();  // 同じ共有リソース(sum)をロックしたい場合は同じロックオブジェクトを使用する
            var arrayForTotal = Enumerable.Repeat(1, 10);
            Parallel.ForEach(arrayForTotal, i => {
                // この中に同時に入れるのは全スレッドの中で1つだけ
                lock (this.sumLock) {
                    sum += i;
                }
            });
            Debug.WriteLine($"lock: sum={sum}");

            // スレッドローカルな変数を試してみる
            Task.WhenAll(
                Task.Run(() => {
                    ParallelTest.threadLocal.Value = 1;
                    Debug.WriteLine($"ThreadLocal (1) = {ParallelTest.threadLocal.Value}");
                }),
                Task.Run(() => {
                    ParallelTest.threadLocal.Value = 2;
                    Debug.WriteLine($"ThreadLocal (2) = {ParallelTest.threadLocal.Value}");
                }),
                Task.Run(() => {
                    ParallelTest.threadLocal.Value = 3;
                    Debug.WriteLine($"ThreadLocal (3) = {ParallelTest.threadLocal.Value}");
                }))
                .Wait();

            // LINQの並列版: PLINQ: 排他制御は自動でやってくれる
            var rangeFor0to100 = Enumerable.Range(0, 10);
            var sumByLINQ = rangeFor0to100.AsParallel().Sum();
            Debug.WriteLine($"Parallel LINQ: sum={sumByLINQ}");

            // 継続実行タスク
            var task1 = Task<int>.Factory.StartNew(() => Enumerable.Range(1, 100).Sum())
                .ContinueWith(
                    subTask => Debug.WriteLine($"Success1: {subTask.Result}"),
                    TaskContinuationOptions.OnlyOnRanToCompletion
                )
                .ContinueWith(
                    subTask => Debug.WriteLine($"Success2")
                );

            // 失敗してみるテスト
            var task2 = Task<int>.Factory.StartNew(() => throw new NotImplementedException())
                .ContinueWith(
                    subTask => Debug.WriteLine($"Failed: {subTask.Exception}"),
                    TaskContinuationOptions.OnlyOnFaulted
                );

            // 非同期操作開始、キャンセルしてみるテスト
            using (var tokenSource = new System.Threading.CancellationTokenSource()) {
                var token = tokenSource.Token;
                var task3 = Task.Run(() => {
                    while (true) {
                        // 外部からのキャンセル要求があればここで脱出する
                        token.ThrowIfCancellationRequested();

                        Debug.WriteLine("Canceling...");
                        System.Threading.Thread.Sleep(500);
                    }
                }, token).ContinueWith(
                    subTask => Debug.WriteLine($"Canceled: {subTask.IsCanceled}"),
                    TaskContinuationOptions.OnlyOnCanceled
                );
                System.Threading.Thread.Sleep(3000);
                tokenSource.Cancel();
            }

            // [古い] マネージドなスレッドプールを使う (今はもう Task.Run するだけでスレッドプールの管理が上手いことやりくりされるので気にしなくてよい)
            //System.Threading.ThreadPool.GetMinThreads(out int workerThreads, out int completionPortThreads);
            //Debug.WriteLine($"workerThreads={workerThreads}, completionPortThreads={completionPortThreads}");
            //System.Threading.ThreadPool.SetMinThreads(workerThreads * 16, completionPortThreads * 16);
            //// System.Threading.ThreadPool.SetMinThreads(1, 1);
            //System.Threading.ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            //Debug.WriteLine($"workerThreads={workerThreads}, completionPortThreads={completionPortThreads}");
            //foreach (var n in Enumerable.Range(0, 50)) {
            //    // System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(obj => {
            //    Task.Run(() => {
            //        // 時間のかかる処理を別スレッドで実行する
            //        foreach (var i in Enumerable.Range(0, 5)) {
            //            System.Threading.Thread.Sleep(1000);
            //            Debug.WriteLine($"{System.Threading.Thread.CurrentThread.ManagedThreadId}_{n}_{i}");
            //        }
            //    });
            //}

            // Genericなコレクションを使って並列ループしてみるテスト
            var dict = new Dictionary<int, string>();
            Parallel.For(0, 10, i => {
                Debug.WriteLine($"Dictionary: Add: [{i}]");
                dict.Add(i, i.ToString());
                Debug.WriteLine($"Dictionary: Get: [1]={(dict.ContainsKey(1) ? dict[1] : null)}");
            });

            // Concurrentなコレクションを使って並列ループしてみるテスト
            var dictConcurrent = new ConcurrentDictionary<int, string>();
            Parallel.For(0, 10, i => {
                var item = dictConcurrent.GetOrAdd(1, key => {
                    // 複数スレッドから同時に呼ばれると、同じキーに対して複数回 Add が走る可能性あり
                    Debug.WriteLine($"ConcurrentDictionary: Add: {i}");
                    return i.ToString();
                });

                // Add が複数回走ったとしても取得できる値は必ず単一になるように保証されている
                Debug.WriteLine($"ConcurrentDictionary: Get: {item}");
            });
        }

        private void parallelFuncA() {
            throw new NotImplementedException();
        }

        private void parallelFuncB(int arg) {
            throw new NotImplementedException($"arg={arg}");
        }

        private int parallelFuncC() {
            return 0;
        }

    }
}
