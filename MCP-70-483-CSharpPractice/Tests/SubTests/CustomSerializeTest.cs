using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// 自作のシリアライズ対応オブジェクトを作ってみる
    /// </summary>
    public class CustomSerializeTest : IRunnable {

        public void Run() {
            var obj = new SerializableObject() {
                Age = 25,
                Name = "Taro",
            };

            // シリアライズ
            using (var fs = new FileStream("custom.dat", FileMode.OpenOrCreate)) {
                new BinaryFormatter().Serialize(fs, obj);
            }

            // デシリアライズ
            SerializableObject readobj;
            using (var fs = new FileStream("custom.dat", FileMode.Open)) {
                readobj = new BinaryFormatter().Deserialize(fs) as SerializableObject;
                readobj?.Print("DeserializedCustomBinary");
            }
        }

        /// <summary>
        /// バイナリーシリアライズ/デシリアライズ対象のクラス
        /// </summary>
        [Serializable]
        public class SerializableObject : ISerializable {

            public int Age;

            public string Name;

            /// <summary>
            /// 初期化子を使うのに必要
            /// </summary>
            public SerializableObject() {
            }

            /// <summary>
            /// デシリアライズするときに必要
            /// </summary>
            public SerializableObject(SerializationInfo info, StreamingContext context) {
                this.Age = info.GetInt32("Age");
                this.Name = info.GetValue("Name", typeof(string)) as string;

                // これでもよい
                // this.Name = info.GetString("Name");
            }

            /// <summary>
            /// シリアライズするのに必要
            /// </summary>
            /// <param name="info"></param>
            /// <param name="context"></param>
            public void GetObjectData(SerializationInfo info, StreamingContext context) {
                info.AddValue("Age", this.Age);
                info.AddValue("Name", this.Name);
            }

            public void Print(string prefix = "") {
                Debug.WriteLine($"{prefix}: Age={this.Age}, Name={this.Name}");
            }
        }

    }
}
