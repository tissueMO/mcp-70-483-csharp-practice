using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// 弱参照を試してみる
    /// </summary>
    public class WeakReferenceTest : IRunnable {

        public void Run() {
            // ここでデータを読んで、すぐストリームを捨てる
            FileHandlerWrapper reader;
            using (var r = new StreamReader(@"D:\Documents\フリーソフト\a5m2\readme.txt")) {
                reader = new FileHandlerWrapper(r);
                Debug.WriteLine($"WeekReferenceTest [1]: {reader.ReadToEnd().Substring(0, 20)}...");
            }

            // GC実行されるとそのストリームが読めなくなっていることを確認
            Task.Run(() => {
                // 強制GC実行
                GC.Collect();
                Thread.Sleep(1000);

                // Expected: もう破棄されちゃったよーん
                Debug.WriteLine($"WeekReferenceTest [2]: {reader?.ReadToEnd()}");
            });
        }

        /// <summary>
        /// 弱参照を使うラッパークラス
        /// </summary>
        public class FileHandlerWrapper {

            private WeakReference<StreamReader> r;

            private string cache;

            public FileHandlerWrapper(StreamReader r) {
                this.r = new WeakReference<StreamReader>(r);
            }

            public string ReadToEnd() {
                if (this.r != null && this.r.TryGetTarget(out var s)) {
                    // この時点でまだGCが破棄していなければ使える
                    if (string.IsNullOrEmpty(this.cache)) {
                        this.cache = s.ReadToEnd();
                    }
                    return this.cache;
                } else {
                    return "(もう破棄されちゃったよーん)";
                }
            }
        }

    }
}
