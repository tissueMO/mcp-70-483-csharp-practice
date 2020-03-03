using MCP_70_483_CSharpPractice.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// ソートを実行して独自比較処理が行われることを確認する
    /// </summary>
    public class ImplementCompareTest : IRunnable {

        public void Run() {
            var stringList = new List<StringWrapper>() {
                new StringWrapper() { String = "abc5def4ghi3jkl" },
                new StringWrapper() { String = "opq1rst2uvw3xyz" },
            };

            Debug.WriteLine("BeforeSort:");
            foreach (var str in stringList) {
                Debug.WriteLine($"{str.String}");
            }

            stringList.Sort();

            Debug.WriteLine("AfterSort:");
            foreach (var str in stringList) {
                Debug.WriteLine($"{str.String}");
            }
        }

    }
}
