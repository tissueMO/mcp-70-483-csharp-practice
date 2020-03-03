using MCP_70_483_CSharpPractice.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// using構文を使用して独自破棄処理が行われることを確認する
    /// </summary>
    public class ImplementDisposeTest : IRunnable {

        public void Run() {
            using (var fileManager = new FileWrapper(@"C:\c\tomcat.log")) {
                fileManager.File.Write(new byte[0], 0, 0);
            }
        }

    }
}
