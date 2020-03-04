using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    public class NLogLoggingTest : IRunnable {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public void Run() {
            NLogLoggingTest.logger.Trace("ログレベル:トレース");
            NLogLoggingTest.logger.Debug("ログレベル:デバッグ");
            NLogLoggingTest.logger.Info("ログレベル:情報");
            NLogLoggingTest.logger.Warn("ログレベル:警告");
            NLogLoggingTest.logger.Error("ログレベル:エラー");
            NLogLoggingTest.logger.Fatal("ログレベル:致命的エラー");
        }

    }
}
