using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace MCP_70_483_CSharpPractice {

    static class Program {

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // 集約例外の捕捉
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (sender, e) => {
                Debug.WriteLine(e.Exception);
                MessageBox.Show("Application.ThreadException: UIスレッド内で捕捉されなかった例外がありました", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            };

            // UIスレッド以外もすべて対象
            AppDomain.CurrentDomain.UnhandledException += (sender, e) => {
                Debug.WriteLine($"{(e.IsTerminating ? "Terminating" : "Not Terminating")}: {e.ExceptionObject}");
                MessageBox.Show($"AppDomain.CurrentDomain.UnhandledException: [{(e.IsTerminating ? "Terminating" : "Not Terminating")}] アプリ内で捕捉されなかった例外がありました", "", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
            };

            Application.Run(new Main());
        }

    }
}
