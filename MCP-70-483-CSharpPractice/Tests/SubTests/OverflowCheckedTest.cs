using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// 算術演算オーバーフローの扱いを区別してみる
    /// </summary>
    public class OverflowCheckedTest : IRunnable {

        public void Run() {
            // デフォルトは unchecked
            // オーバーフローするまで累乗し続ける
            for (int i = 2; 0 < i; i *= i) {
                Thread.Yield();
                Debug.WriteLine($"unchecked-default: {i}");
            }

            // 明示的に unchecked
            for (int i = 2; 0 < i; i = unchecked(i * i)) {
                Thread.Yield();
                Debug.WriteLine($"unchecked: {i}");
            }

            // 明示的に checked
            try {
                for (int i = 2; 0 < i; i = checked(i * i)) {
                    Thread.Yield();
                    Debug.WriteLine($"checked: {i}");
                }
            } catch (OverflowException) {
                Debug.WriteLine("checked: オーバーフローしました");
            }
        }
    }
}
