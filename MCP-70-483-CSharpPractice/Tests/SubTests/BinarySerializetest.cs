using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// バイナリーデータとしてのシリアライズ・デシリアライズを試してみる
    /// </summary>
    public class BinarySerializeTest : IRunnable {

        public void Run() {
            var obj = new CMSLayout() {
                Name = "トップページ",
                Description = "総合ポータルのトップ",
                Template = new CMSLayout.CMSTemplate() {
                    Name = "トップページのテンプレート",
                    FileName = "template_top.html",
                    Aligns = new List<CMSLayout.CMSAlign>() {
                        new CMSLayout.CMSAlign() {
                            Name = "HEAD",
                            Blocks = new List<CMSLayout.CMSBlock>() {
                                new CMSLayout.CMSBlock() {
                                    Name = "ポータルHEAD",
                                    FileName = "block_top_head.html",
                                },
                            },
                        },
                        new CMSLayout.CMSAlign() {
                            Name = "BODY",
                            Blocks = new List<CMSLayout.CMSBlock>() {
                                new CMSLayout.CMSBlock() {
                                    Name = "ポータルBODY",
                                    FileName = "block_top_body.html",
                                },
                            },
                        },
                        new CMSLayout.CMSAlign() {
                            Name = "FOOT",
                            Blocks = new List<CMSLayout.CMSBlock>() {
                                new CMSLayout.CMSBlock() {
                                    Name = "ポータルFOOT",
                                    FileName = "block_top_foot.html",
                                },
                            },
                        },
                    },
                },
                Comment = "レイアウトコメント",
            };

            // オブジェクトをシリアライズ
            using (var fs = new FileStream("cmslayout.dat", FileMode.OpenOrCreate)) {
                new BinaryFormatter().Serialize(fs, obj);
            }

            // ファイルに書き出したバイナリーオブジェクトをデシリアライズ
            CMSLayout readobj;
            using (var fs = new FileStream("cmslayout.dat", FileMode.Open)) {
                readobj = new BinaryFormatter().Deserialize(fs) as CMSLayout;
            }

            // デシリアライズしたオブジェクトを出力
            Debug.WriteLine($"DeserializedBinary: LayoutName={readobj?.Name}, Description={readobj?.Description}, " +
                            $"TemplateName={readobj?.Template?.Name}, TemplateFileName={readobj?.Template?.FileName}, " +
                            $"Comment={readobj?.Comment}");
            foreach (var align in readobj.Template.Aligns) {
                Debug.WriteLine($"DeserializedBinary: AlignName={align?.Name}");

                foreach (var block in align.Blocks) {
                    Debug.WriteLine($"DeserializedBinary: BlockName={block?.Name}, BlockFileName={block?.FileName}");
                }
            }
        }

        /// <summary>
        /// バイナリーシリアライズ/デシリアライズ対象のクラス
        /// </summary>
        [Serializable]
        public class CMSLayout {

            [Serializable]
            public class CMSAlign {

                public string Name;

                public List<CMSBlock> Blocks;

            }

            [Serializable]
            public class CMSBlock {

                public string Name;

                public string FileName;

            }

            [Serializable]
            public class CMSTemplate {

                public string Name;

                public string FileName;

                public List<CMSAlign> Aligns;

            }

            public string Name;

            public string Description;

            public CMSTemplate Template;

            /// <summary>
            /// あえてシリアライズ対象から外してみる
            /// </summary>
            [NonSerialized]
            public string Comment;

        }
    }
}
