using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using MCP_70_483_CSharpPractice.Tests.SubTests;

namespace MCP_70_483_CSharpPractice.Tests {

    /// <summary>
    /// テスト実行用静的クラス
    /// </summary>
    public static class MainTester {

        /// <summary>
        /// 一連のテストタスクを実行します。
        /// </summary>
        public static void RunTests() {
            new DynamicCodeTest().Run();
            new NotNullParameterValidationTest().Run();
            new ParallelTest().Run();
            new NamedParameterTest().Run();
            new IndexerTest().Run();
            new ImplementCompareTest().Run();
            new ImplementDisposeTest().Run();
            new ImplementEnumerateTest().Run();
            new EnumFormatStringTest().Run();
            new StringFormatTest().Run();
            new IntegerOriginalExtensionTest().Run();
        }

    }
}
