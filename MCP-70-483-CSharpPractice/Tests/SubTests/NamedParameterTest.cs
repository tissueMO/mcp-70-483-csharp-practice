using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// 名前付き引数を試してみる
    /// </summary>
    public class NamedParameterTest : IRunnable {

        public void Run() {
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

    }
}
