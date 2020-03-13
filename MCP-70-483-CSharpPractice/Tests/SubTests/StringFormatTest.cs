using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// フォーマット文字列を試してみる
    /// </summary>
    public class StringFormatTest : IRunnable {

        public void Run() {
            Debug.WriteLine(string.Format("任意の値の埋め込み: {0}, {1}, ...", 3.14, "TEST"));
            Debug.WriteLine(string.Format("表示幅の指定はカンマで: 5文字分右寄せ [{0,5}]", "ABC"));
            Debug.WriteLine(string.Format("表示幅の指定はカンマで: 5文字分左寄せ [{0,-5}]", "ABC"));
            Debug.WriteLine(string.Format("書式指定: 通貨=C [{0:C}]", 1124));
            Debug.WriteLine(string.Format("書式指定: 指数表記=E [{0:E}]", 1124));
            Debug.WriteLine(string.Format("書式指定: 固定小数=F桁数 [{0:F3}]", 3.14159));
            Debug.WriteLine(string.Format("書式指定: 数値3ケタ区切り=N桁数 [{0:N0}]", 1234567.89));
            Debug.WriteLine(string.Format("書式指定: ％=P桁数 [{0:P1}]", 0.245));
            Debug.WriteLine(string.Format("書式指定: 16進数=X [{0:X}]", 255));

            // 自作フォーマットを適用してみる
            var formatter = new OriginalObjectFormatter();
            OriginalObject obj;
            obj = new OriginalObject() { Number = 0 };
            Debug.WriteLine(string.Format(formatter, "書式指定<独自>: 10進数 0=[{0:D}]", obj));
            obj = new OriginalObject() { Number = 1 };
            Debug.WriteLine(string.Format(formatter, "書式指定<独自>: 10進数 1=[{0:D}]", obj));
            obj = new OriginalObject() { Number = 2 };
            Debug.WriteLine(string.Format(formatter, "書式指定<独自>: 16進数 2=[{0:X}]", obj));
            obj = new OriginalObject() { Number = 3 };
            Debug.WriteLine(string.Format(formatter, "書式指定<独自>: 16進数 3=[{0:X}]", obj));
            obj = new OriginalObject() { Number = 4 };
            Debug.WriteLine($"書式指定<独自>: 10進数 4=[{obj:D}]");
        }

    }

    /// <summary>
    /// 独自フォーマット対応するクラス
    /// </summary>
    public class OriginalObject : IFormattable {

        public int Number {
            get; set;
        }

        /// <summary>
        /// 自作フォーマットした文字列を返す
        /// 文字列補間、単なるToStringした際に呼び出される
        /// </summary>
        public string ToString(string format, IFormatProvider formatProvider) {
            var formatter = new OriginalObjectFormatter();
            return formatter.Format(format, this, formatter);
        }

    }

    /// <summary>
    /// 独自フォーマッター
    /// </summary>
    public class OriginalObjectFormatter : IFormatProvider, ICustomFormatter {
        
        /// <summary>
        /// フォーマットする対象の型であるかどうかを判定
        /// </summary>
        public object GetFormat(Type formatType) {
            Debug.WriteLine($"IFormatProvider: 自作フォーマット判定 {formatType.Name}");
            return formatType == typeof(OriginalObject) ? this : null;
        }

        /// <summary>   
        /// フォーマットした文字列を返す
        /// </summary>
        public string Format(string format, object arg, IFormatProvider formatProvider) {
            Debug.WriteLine($"CustomFormatter: 自作フォーマットするよ {format}");
            var obj = arg as OriginalObject;
            string prefix = format == "X" ? "0x" : "";
            switch (obj?.Number) {
                case 0:
                    return $"{prefix}ZERO";
                case 1:
                    return $"{prefix}ONE";
                case 2:
                    return $"{prefix}TWO";
                case 3:
                    return $"{prefix}THREE";
                default:
                    return $"{prefix}OTHER";
            }
        }
        
    }

}
