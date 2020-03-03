using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Extensions {

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

}
