using MCP_70_483_CSharpPractice.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// Integerの自作拡張メソッドを実行してみる
    /// </summary>
    public class IntegerOriginalExtensionTest : IRunnable {

        public void Run() {
            Debug.WriteLine($"SumExtendTest: x=1, y=2 -> {1.SumExtendTest(2)}");
        }

    }
}
