using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCP_70_483_CSharpPractice {

    /// <summary>
    /// 拡張メソッドを試してみる
    /// </summary>
    public static class IntegerOriginalExtension {
        /// <summary>
        /// x が主語になる拡張メソッド
        /// </summary>
        public static int SumExtendTest(this int x, int y) {
            return x + y;
        }
    }

    public partial class Main : Form {

        public Main() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            this.parallelTest();
            this.parameterTest();
            this.indexerTest();
            System.Diagnostics.Debug.WriteLine($"SumExtendTest: x=1, y=2 -> {1.SumExtendTest(2)}");
        }

        private Dictionary<string, object> objDictionary = new Dictionary<string, object>();

        public object this[string key] {
            get => (this.objDictionary.ContainsKey(key) ? this.objDictionary[key] : null);
            set => this.objDictionary[key] = value;
        }

        private void indexerTest() {
            this["TEST"] = "testValue";
            System.Diagnostics.Debug.WriteLine($"this[test]={this["test"]}");
            System.Diagnostics.Debug.WriteLine($"this[TEST]={this["TEST"]}");
        }

        private void parameterTest() {
            this.namedParameterTest(1, b: 2);
            this.namedParameterTest(a: 1, 2);
            this.namedParameterTest(b: 2, a: 1);
            this.namedParameterTest(b: 2, a: 1, mode: false);  // 名前付き引数だけであれば順序入れ替えOK
            this.namedParameterTest(a: 2, b: 2, true);  // 名前付き引数と位置引数を混ぜてもOK
            // this.namedParameterTest(b: 2, 1);  // 位置引数と名前付き引数を混ぜて順序入れ替えた結果重複しちゃうのでNG
            // this.namedParameterTest(b: 2, a: 1, false); // 位置引数と名前付き引数を混ぜて順序入れ替えるのはNG
        }

        private int namedParameterTest(int a, int b, bool mode = false) {
            if (mode) {
                return a + b;
            } else {
                return a - b;
            }
        }

        private void parallelTest() {
            System.Diagnostics.Debug.WriteLine("並列処理のテスト");

            // 単純な関数の並列呼び出し
            // Parallel.Invoke(parallelFuncA, parallelFuncB, parallelFuncC);  // AはOK、Bは引数あるのでNG、Cは戻り値がvoidじゃないのでNG

            // for文の並列版
            Parallel.For(0, 10, i => System.Diagnostics.Debug.WriteLine($"Parallel.For: {i}"));  // 0-9 までの数字が出力される

            // foreach文の並列版
            var rangeFor0to10 = Enumerable.Range(0, 10);
            Parallel.ForEach(rangeFor0to10, i => System.Diagnostics.Debug.WriteLine($"Parallel.ForEach: {i}"));  // 0-9 までの数字が出力される

            // lockステートメント
            var sum = 0;
            var sumLock = new object();  // 同じ共有リソース(sum)をロックしたい場合は同じロックオブジェクトを使用する
            var arrayForTotal = Enumerable.Repeat(1, 10);
            Parallel.ForEach(arrayForTotal, i => {
                lock (sumLock) {
                    sum += i;
                }
            });
            System.Diagnostics.Debug.WriteLine($"lock: sum={sum}");

            // LINQの並列版: PLINQ: 排他制御は自動でやってくれる
            var rangeFor0to100 = Enumerable.Range(0, 10);
            var sumByLINQ = rangeFor0to100.AsParallel().Sum();
            System.Diagnostics.Debug.WriteLine($"Parallel LINQ: sum={sumByLINQ}");

            // 継続実行タスク
            var task1 = Task<int>.Factory.StartNew(() => Enumerable.Range(1, 100).Sum())
                .ContinueWith(
                    subTask => System.Diagnostics.Debug.WriteLine($"Success1: {subTask.Result}"),
                    TaskContinuationOptions.OnlyOnRanToCompletion
                )
                .ContinueWith(
                    subTask => System.Diagnostics.Debug.WriteLine($"Success2")
                );

            // 失敗してみるテスト
            var task2 = Task<int>.Factory.StartNew(() => throw new NotImplementedException())
                .ContinueWith(
                    subTask => System.Diagnostics.Debug.WriteLine($"Failed: {subTask.Exception}"),
                    TaskContinuationOptions.OnlyOnFaulted
                );

            // 非同期操作開始、キャンセルしてみるテスト
            using (var tokenSource = new System.Threading.CancellationTokenSource()) {
                var token = tokenSource.Token;
                var task3 = Task.Run(() => {
                    while (true) {
                        // 外部からのキャンセル要求があればここで脱出する
                        token.ThrowIfCancellationRequested();

                        System.Diagnostics.Debug.WriteLine("Canceling...");
                        System.Threading.Thread.Sleep(500);
                    }
                }, token).ContinueWith(
                    subTask => System.Diagnostics.Debug.WriteLine($"Canceled: {subTask.IsCanceled}"),
                    TaskContinuationOptions.OnlyOnCanceled
                );
                System.Threading.Thread.Sleep(3000);
                tokenSource.Cancel();
            }

            // [古い] マネージドなスレッドプールを使う (今はもう Task.Run するだけでスレッドプールの管理が上手いことやりくりされるので気にしなくてよい)
            //System.Threading.ThreadPool.GetMinThreads(out int workerThreads, out int completionPortThreads);
            //System.Diagnostics.Debug.WriteLine($"workerThreads={workerThreads}, completionPortThreads={completionPortThreads}");
            //System.Threading.ThreadPool.SetMinThreads(workerThreads * 16, completionPortThreads * 16);
            //// System.Threading.ThreadPool.SetMinThreads(1, 1);
            //System.Threading.ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
            //System.Diagnostics.Debug.WriteLine($"workerThreads={workerThreads}, completionPortThreads={completionPortThreads}");
            //foreach (var n in Enumerable.Range(0, 50)) {
            //    // System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(obj => {
            //    Task.Run(() => {
            //        // 時間のかかる処理を別スレッドで実行する
            //        foreach (var i in Enumerable.Range(0, 5)) {
            //            System.Threading.Thread.Sleep(1000);
            //            System.Diagnostics.Debug.WriteLine($"{System.Threading.Thread.CurrentThread.ManagedThreadId}_{n}_{i}");
            //        }
            //    });
            //}

            // Genericなコレクションを使って並列ループしてみるテスト
            var dict = new Dictionary<int, string>();
            Parallel.For(0, 10, i => {
                System.Diagnostics.Debug.WriteLine($"Dictionary: Add: [{i}]");
                dict.Add(i, i.ToString());
                System.Diagnostics.Debug.WriteLine($"Dictionary: Get: [1]={(dict.ContainsKey(1) ? dict[1] : null)}");
            });

            // Concurrentなコレクションを使って並列ループしてみるテスト
            var dictConcurrent = new ConcurrentDictionary<int, string>();
            Parallel.For(0, 10, i => {
                var item = dictConcurrent.GetOrAdd(1, key => {
                    // 複数スレッドから同時に呼ばれると、同じキーに対して複数回 Add が走る可能性あり
                    System.Diagnostics.Debug.WriteLine($"ConcurrentDictionary: Add: {i}");
                    return i.ToString();
                });

                // Add が複数回走ったとしても取得できる値は必ず単一になるように保証されている
                System.Diagnostics.Debug.WriteLine($"ConcurrentDictionary: Get: {item}");
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

        private void button1_Click(object sender, EventArgs e) {
            // ゼロ除算で強制ランタイムエラー
            var temp = 1 / new Random().Next(1);
        }

        private void button2_Click(object sender, EventArgs e) {
            // UIスレッド外での未捕捉の例外テスト
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(obj => {
                var temp = 1 / new Random().Next(1);
                // throw new NotImplementedException();
            }));

            // 以下では例外が握りつぶされる仕様らしい
            //Task.Run(() => {
            //    // 強制的にメモリ不足を引き起こさせる
            //    var list = new List<char[]>();
            //    while (true) {
            //        var buf = new char[16777216];
            //        list.Add(buf);
            //    }
            //});
        }
    }
}
