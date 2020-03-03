using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// フォーマット文字列を試してみる
    /// </summary>
    public class StringFormatTest : IRunnable {

        public void Run() {
            Debug.WriteLine(string.Format("任意の値の埋め込み: {0}, {1}, ...", 3.14, "TEST"));
            Debug.WriteLine(string.Format("表示幅の指定はカンマで: 5文字分右寄せ [{0,5}]", "ABC"));
            Debug.WriteLine(string.Format("表示幅の指定はカンマで: 5文字分左寄せ [{0,-5}]", "ABC"));
            Debug.WriteLine(string.Format("書式指定: 通貨=C [{0:C}]", 1124));
            Debug.WriteLine(string.Format("書式指定: 指数表記=E [{0:E}]", 1124));
            Debug.WriteLine(string.Format("書式指定: 固定小数=F桁数 [{0:F3}]", 3.14159));
            Debug.WriteLine(string.Format("書式指定: 数値3ケタ区切り=N桁数 [{0:N0}]", 1234567.89));
            Debug.WriteLine(string.Format("書式指定: ％=P桁数 [{0:P1}]", 0.245));
            Debug.WriteLine(string.Format("書式指定: 16進数=X [{0:X}]", 255));
        }

    }
}
