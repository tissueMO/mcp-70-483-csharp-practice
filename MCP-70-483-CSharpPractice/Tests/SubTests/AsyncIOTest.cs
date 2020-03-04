using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// 非同期I/Oを試してみる
    /// </summary>
    public class AsyncIOTest : IRunnable {

        public void Run() {
            this.asymmetricalRead();
        }

        /// <summary>
        /// デカイテキストファイルを非同期でロードしてみる
        /// </summary>
        private async void asymmetricalRead() {
            string text;
            using (var r = new StreamReader(@"\\DEVSTOR2\Projects\AG6647_BIGDATA\99_調査_試作_提案等\アスクル名刺解析\フォント識別\フォントビットマップ\for_train_MSゴシック.txt")) {
                text = await r.ReadToEndAsync();
            }
            Debug.WriteLine($"AsyncTextLoad: {text.Substring(0, 20)}...");
        }

    }
}
