using MCP_70_483_CSharpPractice.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// foreach文を使用して独自イテレーション処理が行われることを確認する
    /// </summary>
    public class ImplementEnumerateTest : IRunnable {

        public void Run() {
            var list = new MyList() {
                innerList = new List<string>() {
                    "TEST-001",
                    "TEST-002",
                    "TEST-003",
                    "TEST-004",
                    "TEST-005",
                    "TEST-006",
                    "TEST-007",
                    "TEST-008",
                    "TEST-009",
                    "TEST-010",
                }
            };

            foreach (var item in list) {
                Debug.WriteLine($"Enumerate: {item}");
            }
        }

    }
}
