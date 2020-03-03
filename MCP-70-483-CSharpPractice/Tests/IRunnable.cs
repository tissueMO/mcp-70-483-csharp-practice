using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests {

    /// <summary>
    /// 引数無し、戻り値無しで単に実行可能であることを表します。
    /// </summary>
    interface IRunnable {

        /// <summary>
        /// 引数無し、戻り値無しで固有の処理を実行します。
        /// </summary>
        void Run();

    }
}
