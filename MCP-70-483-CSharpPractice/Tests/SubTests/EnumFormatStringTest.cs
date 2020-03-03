using System;
using System.Collections.Generic;
using System.Drawing;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// 列挙型フォーマット文字列を試してみる
    /// </summary>
    public class EnumFormatStringTest : IRunnable {

        public void Run() {
            Debug.WriteLine($"MiddleLeft [G]: {ContentAlignment.MiddleLeft.ToString("G")}");
            Debug.WriteLine($"MiddleLeft|TopRight [G]: {(ContentAlignment.MiddleLeft | ContentAlignment.TopRight).ToString("G")}");
            Debug.WriteLine($"MiddleLeft|TopRight [F]: {(ContentAlignment.MiddleLeft | ContentAlignment.TopRight).ToString("F")}");
            Debug.WriteLine($"MiddleLeft|TopRight [D]: {(ContentAlignment.MiddleLeft | ContentAlignment.TopRight).ToString("D")}");
            Debug.WriteLine($"MiddleLeft|TopRight [X]: {(ContentAlignment.MiddleLeft | ContentAlignment.TopRight).ToString("X")}");
        }

    }
}
