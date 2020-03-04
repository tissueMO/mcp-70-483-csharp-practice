using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// パフォーマンスカウンターを試してみる
    /// </summary>
    public class PerformanceCounterTest : IRunnable {

        /// <summary>
        /// 1秒ごとにCPU使用率を出してみる
        /// </summary>
        public void Run() {
            using (var pc = new PerformanceCounter("Processor", "% Processor Time", "_Total")) {
                Thread.Sleep(1000);

                foreach (var i in Enumerable.Range(0, 5)) {
                    Debug.WriteLine($"CPU Usage={pc.NextValue()}");
                    Thread.Sleep(1000);
                }
            }
        }

    }
}
