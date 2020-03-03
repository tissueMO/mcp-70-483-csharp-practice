using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// 動的に C# コードを生成して Hello World してみる
    /// </summary>
    public class DynamicCodeTest : IRunnable {

        public void Run() {
            // コード生成
            var nameSpace = new CodeNamespace("HelloWorld");

            var mainClass = new CodeTypeDeclaration("MainClass");
            nameSpace.Types.Add(mainClass);

            var mainMethod = new CodeMemberMethod() {
                Attributes = MemberAttributes.Public | MemberAttributes.Static,
                Name = "MainMethod",
                ReturnType = new CodeTypeReference(typeof(string)),
            };
            mainMethod.Statements.Add(new CodeMethodReturnStatement(
                new CodePrimitiveExpression("Hello World. by dynamic generated code.")
            ));
            mainClass.Members.Add(mainMethod);

            // アセンブリ生成
            var codeCompileUnit = new CodeCompileUnit();
            codeCompileUnit.Namespaces.Add(nameSpace);
            codeCompileUnit.ReferencedAssemblies.Add("System.dll");
            var compileResults = CodeDomProvider.CreateProvider("C#").CompileAssemblyFromDom(new CompilerParameters {
                GenerateInMemory = true
            }, codeCompileUnit);

            // メモリ内で生成したアセンブリを実行
            var asm = compileResults.CompiledAssembly;
            var entryClass = asm.GetType("HelloWorld.MainClass");
            var entryMethod = entryClass?.GetMethod("MainMethod");

            // メソッドを実行し、戻り値を受け取る
            var result = entryMethod.Invoke(null, null) as string;
            Debug.WriteLine($"DynamicCodeResult: {result}");
        }

    }
}
