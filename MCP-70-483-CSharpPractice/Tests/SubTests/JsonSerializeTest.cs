using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// 標準ライブラリーでJSONシリアライズ/デシリアライズを試してみる
    /// </summary>
    public class JsonSerializeTest : IRunnable {

        public void Run() {
            // デシリアライズ
            using (var R = new StreamReader("Resources/CMSLayout.json")) {
                var json = R.ReadToEnd();
                using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json))) {
                    var serializer = new DataContractJsonSerializer(typeof(CMSLayout));
                    var readobj = serializer.ReadObject(ms) as CMSLayout;

                    Debug.WriteLine($"Deserialized: LayoutName={readobj?.Name}, Description={readobj?.Description}, " +
                                    $"TemplateName={readobj?.Template?.Name}, TemplateFileName={readobj?.Template?.FileName}, " +
                                    $"Comment={readobj?.Comment}");

                    foreach (var align in readobj.Template.Aligns) {
                        Debug.WriteLine($"Deserialized: AlignName={align?.Name}");

                        foreach (var block in align.Blocks) {
                            Debug.WriteLine($"Deserialized: BlockName={block?.Name}, BlockFileName={block?.FileName}");
                        }
                    }
                }
            }

            // シリアライズ
            using (var ms = new MemoryStream()) {
                var serializer = new DataContractJsonSerializer(typeof(CMSLayout));
                var writeobj = new CMSLayout();
                serializer.WriteObject(ms, writeobj);
                Debug.WriteLine($"Serialized: {Encoding.UTF8.GetString(ms.ToArray())}");
            }
        }

        /// <summary>
        /// JSONシリアライズ/デシリアライズ対象のクラス
        /// </summary>
        [DataContract]
        public class CMSLayout {

            [DataContract]
            public class CMSAlign {

                [DataMember(Name = "name")]
                public string Name {
                    get; set;
                } = "";

                [DataMember(Name = "blocks")]
                public List<CMSBlock> Blocks {
                    get; set;
                } = new List<CMSBlock>();

            }

            [DataContract]
            public class CMSBlock {

                [DataMember(Name = "name")]
                public string Name {
                    get; set;
                } = "";

                [DataMember(Name = "file")]
                public string FileName {
                    get; set;
                } = "";

            }

            [DataContract]
            public class CMSTemplate {

                [DataMember(Name = "name")]
                public string Name {
                    get; set;
                } = "";

                [DataMember(Name = "file")]
                public string FileName {
                    get; set;
                } = "";

                [DataMember(Name = "aligns")]
                public List<CMSAlign> Aligns {
                    get; set;
                } = new List<CMSAlign>();

            }

            [DataMember(Name = "name")]
            public string Name {
                get; set;
            } = "";

            [DataMember(Name = "description")]
            public string Description {
                get; set;
            } = "";

            [DataMember(Name = "template")]
            public CMSTemplate Template {
                get; set;
            } = new CMSTemplate();

            /// <summary>
            /// あえてシリアライズ対象から外してみる
            /// </summary>
            public string Comment {
                get; set;
            } = "";

        }
    }
}
