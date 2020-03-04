using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// XMLのシリアライズ・デシリアライズを試してみる
    /// </summary>
    public class XmlSerializeTest : IRunnable {

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

            // 特定の型におけるXMLのシリアライザーを生成
            var serializer = new XmlSerializer(typeof(CMSLayout));

            // シリアライズ
            using (var fs = new FileStream("cmslayout.xml", FileMode.OpenOrCreate))
            using (var w = new StreamWriter(fs)) {
                serializer.Serialize(w, obj);
            }

            // デシリアライズ
            CMSLayout readobj;
            using (var ms = new FileStream("cmslayout.xml", FileMode.Open))
            using (var r = new StreamReader(ms)) {
                readobj = serializer.Deserialize(r) as CMSLayout;
            }

            // デシリアライズしたオブジェクトを出力
            Debug.WriteLine($"DeserializedXML: LayoutName={readobj?.Name}, Description={readobj?.Description}, " +
                            $"TemplateName={readobj?.Template?.Name}, TemplateFileName={readobj?.Template?.FileName}, " +
                            $"Comment={readobj?.Comment}");
            foreach (var align in readobj.Template.Aligns) {
                Debug.WriteLine($"DeserializedXML: AlignName={align?.Name}");

                foreach (var block in align.Blocks) {
                    Debug.WriteLine($"DeserializedXML: BlockName={block?.Name}, BlockFileName={block?.FileName}");
                }
            }
        }

        /// <summary>
        /// バイナリーシリアライズ/デシリアライズ対象のクラス
        /// </summary>
        public class CMSLayout {

            public class CMSAlign {

                public string Name;

                public List<CMSBlock> Blocks;

            }

            public class CMSBlock {

                public string Name;

                public string FileName;

            }

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
            [XmlIgnore]
            public string Comment;

        }
    }
}
