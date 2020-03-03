using MCP_70_483_CSharpPractice.Tests;
using System;
using System.Windows.Forms;

namespace MCP_70_483_CSharpPractice {

    /// <summary>
    /// メインクラス
    /// </summary>
    public partial class Main : Form {

        public Main() {
            this.InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            MainTester.RunTests();
        }

        private void button1_Click(object sender, EventArgs e) {
            // ゼロ除算で強制ランタイムエラー
            string.Format("{0}", 1 / new Random().Next(1/*常にゼロを返す*/));
        }

        private void button2_Click(object sender, EventArgs e) {
            // UIスレッド外での未捕捉の例外テスト
            System.Threading.ThreadPool.QueueUserWorkItem(new System.Threading.WaitCallback(obj => {
                // ゼロ除算で強制ランタイムエラー
                var temp = 1 / new Random().Next(1/*常にゼロを返す*/);
            }));

            // 以下では例外が握りつぶされる仕様らしい
            //Task.Run(() => {
            //    // 強制的にメモリ不足を引き起こさせる
            //    var list = new List<char[]>();
            //    while (true) {
            //        var buf = new char[16777216];
            //        list.Add(buf);
            //    }
            //});
        }

    }
}
