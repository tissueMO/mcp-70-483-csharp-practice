using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Attributes {

    /// <summary>
    /// 自作属性を試してみる
    /// パラメーターにこの属性を仕込んだものについて、Nullチェックを行えるようにする
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class NonNullParameterAttribute : Attribute {

        /// <summary>
        /// この属性の基本機能
        /// </summary>
        private bool validate(object obj) {
            return !(obj is null);
        }

        /// <summary>
        /// 型情報、メソッド名、引数の値を渡すことで自作属性が付いているものについて、Nullチェックを行う
        /// </summary>
        public static bool Validate(Type type, string methodName, object[] arguments) {
            var methodInfo = type.GetMethod(methodName, BindingFlags.InvokeMethod | BindingFlags.NonPublic | BindingFlags.Instance);

            if (methodInfo.GetParameters().Length != arguments.Length) {
                throw new ArgumentException("この関数が持つ引数の数と、バリデーション対象の引数の数が一致しません。", "arguments");
            }

            for (var i = 0; i < arguments.Length; i++) {
                var param = methodInfo.GetParameters()[i];
                var arg = arguments[i];

                if (param.GetCustomAttributes(typeof(NonNullParameterAttribute), true).Length == 0) {
                    continue;
                } else if (!(param.GetCustomAttribute(typeof(NonNullParameterAttribute), true)
                        as NonNullParameterAttribute).validate(arg)) {
                    return false;
                }
            }

            return true;
        }

    }

}
