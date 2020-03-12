using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    public class ConstructorTest : IRunnable {

        public void Run() {
            throw new NotImplementedException();
        }

    }

    public class Employee {

        public string Name {
            get; set;
        }

        public string Department {
            get; set;
        }

        protected Employee() : this("", "") {
        }

        // 以下のように、デフォルトコンストラクターを private にしてしまうと、派生クラスのデフォルトコンストラクターを作れなくなる
        //private Employee() : this("", "") {
        //}

        public Employee(string name, string department) {
            this.Name = name;
            this.Department = department;
        }

    }

    public class Manager : Employee {
        public Manager() {
        }

        // 以下のように明示的にコンストラクターを区別してやれば、親クラスのデフォルトコンストラクターが private であってもコンパイルは通る
        // が、これでは DRY の原則が破綻する (親クラスでやるべき初期化を子クラスに担わせている) ので設計上NG
        //public Manager() : base("", "") {
        //}

        public Manager(string name, string department) : base(name, department) { }
    }
}
