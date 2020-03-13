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

            /// <summary>
            /// インターフェースの明示的な実装 その1
            /// IEnumerator に明示的にキャストしないとこの Current は参照できない
            /// </summary>
            object IEnumerator.Current {
                get {
                    return (this.index < this.innerList.Count) ? this.innerList[this.index] : null;
                }
            }

            /// <summary>
            /// インターフェースの明示的な実装 その2
            /// IEnumerator<string> に明示的にキャストしないとこの Current は参照できない
            /// </summary>
            string IEnumerator<string>.Current {
                get {
                    return ((IEnumerator)this).Current as string;
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

        /// <summary>
        /// インターフェースの明示的な実装
        /// IEnumerator に明示的にキャストしないと呼び出せないようにする
        /// しかも public ではないので外からは隠蔽したことにする
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator() {
            return this.GetEnumerator();
        }

    }

}
