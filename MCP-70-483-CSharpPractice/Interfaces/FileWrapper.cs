using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Interfaces {

    /// <summary>
    /// IDisposable を実装してみる
    /// </summary>
    public class FileWrapper : IDisposable {

        public FileStream File {
            get;
            private set;
        }

        public FileWrapper(string path) {
            this.File = System.IO.File.Open(path, FileMode.Open);
        }

        public void Dispose() {
            this.File.Close();
        }

    }

}
