using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// インデクサーを試してみる
    /// ついでに get/set をラムダ式で定義してみる
    /// </summary>
    public class IndexerTest : IRunnable {

        public void Run() {
            this["TEST"] = "testValue";
            Debug.WriteLine($"this[test]={this["test"]}");
            Debug.WriteLine($"this[TEST]={this["TEST"]}");
        }

        /// <summary>
        /// インデクサーで対象にする辞書
        /// </summary>
        private Dictionary<string, object> objDictionary = new Dictionary<string, object>();

        public object this[string key] {
            get => this.objDictionary.ContainsKey(key) ? this.objDictionary[key] : null;
            set => this.objDictionary[key] = value;
        }

    }
}
