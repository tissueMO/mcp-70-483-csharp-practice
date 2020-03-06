using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// 実行時に動的型付けをする仕組みを試してみる
    /// </summary>
    public class DynamicTypeTest : IRunnable {

        public void Run() {
            this.printDynamicType("TEST");
            this.printDynamicType(1);
            this.printDynamicType(1.2f);
            this.printDynamicType(new object());
        }

        /// <summary>
        /// 実行するまで obj の型が何であるかはわからない
        /// 型判定してキャストすることは可能
        /// </summary>
        public void printDynamicType(dynamic obj) {
            Debug.WriteLine($"Dynamic: {obj}");

            if (obj is string) {
                Debug.WriteLine($"Dynamic: string");
            } else if (obj is int) {
                Debug.WriteLine($"Dynamic: int");
            } else if (obj is float) {
                Debug.WriteLine($"Dynamic: float");
            } else if (obj is object) {
                Debug.WriteLine($"Dynamic: object");
            }
        }

    }

}
