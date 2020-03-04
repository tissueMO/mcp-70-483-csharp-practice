using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// イベントログの書き込みテスト
    /// 
    /// 管理者権限を有していない場合 System.Security.SecurityException を吐かれて実行できない。
    /// マニフェストファイルから管理者特権昇格を認めてもらうよう促す設定を追加する必要がある。
    /// </summary>
    public class WriteToEventLogTest : IRunnable {

        public void Run() {
            const string SourceName = "WriteToEventLogTest by C#";

            if (!EventLog.SourceExists(SourceName)) {
                EventLog.CreateEventSource(SourceName, "App");
            }

            EventLog.WriteEntry(SourceName, "C#コードからのイベントログの書き込みテストです。", EventLogEntryType.Information);
        }

    }
}
