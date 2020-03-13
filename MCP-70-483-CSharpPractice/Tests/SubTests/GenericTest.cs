using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// ジェネリック制約付きのジェネリック型を作ってみる
    /// </summary>
    public class GenericTest : IRunnable {

        public void Run() {
            var test = new GenericTest<InheritClass>();
            test.Execute();
        }
    }

    /// <summary>
    /// T は BaseClass からの継承関係があり、かつ、public な引数無しコンストラクターがあり、かつ Executable であるもののみ指定可能
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericTest<T> where T : BaseClass, IExecutable, new() {

        public GenericTest() {
            // new T(); は where句で ,new() を付けていないとコンパイルが通らない
            // public な引数無しコンストラクターがあることを事前に確定していないといけない
            this.HavingObject = new T();
        }

        public void Execute() {
            this.HavingObject.Execute();
        }

        public T HavingObject {
            get; set;
        }
    }

    public class BaseClass : IExecutable {

        public int Value {
            get;
            private set;
        }

        // where句で new() が指定される場合、引数無しコンストラクターにアクセスできなければコンパイルが通らない
        //private BaseClass() {
        //}
        public BaseClass() { }

        protected BaseClass(int value) {
            this.Value = value;
        }
    
        public virtual void Execute() {
            Debug.WriteLine($"ジェネリックなクラスに持たされた基本クラスが Execute したよ: {this.Value}");
        }
    }

    public class InheritClass : BaseClass {

        // where句で new() が指定される場合、引数無しコンストラクターにアクセスできなければコンパイルが通らない
        public InheritClass() : base(0) { }

        public InheritClass(int value) : base(value) { }

        public override void Execute() {
            Debug.WriteLine($"ジェネリックなクラスに持たされた派生クラスが Execute したよ: {this.Value}");
        }
    }

    public interface IExecutable {

        void Execute();

    }

}
