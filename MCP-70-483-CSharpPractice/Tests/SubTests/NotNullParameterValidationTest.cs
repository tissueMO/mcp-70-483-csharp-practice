using MCP_70_483_CSharpPractice.Attributes;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// 自作属性を試してみる
    /// </summary>
    public class NotNullParameterValidationTest : IRunnable {

        public void Run() {
            this.notNullValidationTest(this, new EventArgs());
            this.notNullValidationTest(null, new EventArgs());
            this.notNullValidationTest(this, null);
        }

        /// <summary>
        /// 引数バリデーションを行うテスト用のメソッド
        /// </summary>
        private void notNullValidationTest([NonNullParameter] object sender, [NonNullParameter] EventArgs e) {
            var isValid = NonNullParameterAttribute.Validate(this.GetType(), MethodBase.GetCurrentMethod().Name, new object[] { sender, e });
            Debug.WriteLine($"NullCheck: {isValid}");
        }

    }
}
