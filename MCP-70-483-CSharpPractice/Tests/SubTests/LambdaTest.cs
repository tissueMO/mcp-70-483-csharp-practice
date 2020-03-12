using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// 非同期ループで回したときに、ループ変数の中身がラムダ式の中でどう扱われるかを試してみる
    /// </summary>
    public class LambdaTest : IRunnable {

        public void Run() {
            // こいつだけが全部 5 になって出てくる
            for (int i = 0; i < 5; i++) {
                Task.Run(() => {
                    Debug.WriteLine($"LambdaTest-for-out: {i}");
                });
            }

            // これ以降はその時点のループ変数が正しく出てくる
            for (int i = 0; i < 5; i++) {
                new Task((ii) => {
                    Debug.WriteLine($"LambdaTest-for-in: {(int) ii}");
                }, i).Start();
            }

            foreach (var i in Enumerable.Range(0, 5)) {
                Task.Run(() => {
                    Debug.WriteLine($"LambdaTest-foreach-out: {i}");
                });
            }

            foreach (var i in Enumerable.Range(0, 5)) {
                new Task((ii) => {
                    Debug.WriteLine($"LambdaTest-foreach-in: {(int) ii}");
                }, i).Start();
            }
        }

    }
}
