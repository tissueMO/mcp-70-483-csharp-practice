using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Interfaces {

    /// <summary>
    /// IEnumerable/IEnumerator を実装してみる
    /// </summary>
    public class MyList : IEnumerable<string> {

        /// <summary>
        /// foreach で使われるイテレーション機構
        /// </summary>
        public class MyListEnumerator : IEnumerator<string> {

            public List<string> innerList = new List<string>();

            private int index = -1;

            public object Current {
                get {
                    return (this.index < this.innerList.Count) ? this.innerList[this.index] : null;
                }
            }

            string IEnumerator<string>.Current {
                get {
                    return this.Current as string;
                }
            }

            public void Dispose() {
            }

            public bool MoveNext() {
                this.index++;
                return this.index < this.innerList.Count;
            }

            public void Reset() {
                this.index = -1;
            }
        }

        public List<string> innerList = new List<string>();

        public IEnumerator<string> GetEnumerator() {
            return new MyListEnumerator() {
                innerList = this.innerList
            };
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

    }

}
