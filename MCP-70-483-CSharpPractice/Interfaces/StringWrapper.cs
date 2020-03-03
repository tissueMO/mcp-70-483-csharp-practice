using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Interfaces {

    /// <summary>
    /// IComparable を実装してみる
    /// 文字列に含まれる数字だけを抽出してソートできるようにする
    /// </summary>
    public class StringWrapper : IComparable {

        public string String {
            get; set;
        }

        public int CompareTo(object obj) {
            var target = obj as StringWrapper;
            var x = int.Parse(new Regex(@"[^0-9]").Replace(this.String, ""));
            var y = int.Parse(new Regex(@"[^0-9]").Replace(target?.String ?? "", ""));
            return x - y;
        }
    }

}
