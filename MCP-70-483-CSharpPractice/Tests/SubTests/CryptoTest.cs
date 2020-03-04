using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    public class CryptoTest : IRunnable {

        public void Run() {
            this.encryptAndDecryptBySymmetrical();
            this.encryptAndDecryptByAsymmetrical();
        }

        /// <summary>
        /// AES方式の共通鍵を使った暗号化・復号化を試してみる
        /// </summary>
        public void encryptAndDecryptBySymmetrical() {
            var csp = new AesCryptoServiceProvider();

            // 共通鍵を生成
            csp.GenerateIV();
            csp.GenerateKey();
            Debug.WriteLine($"Encrypt by AES: IV={Convert.ToBase64String(csp.IV)}, Key={Convert.ToBase64String(csp.Key)}");

            // 共通鍵で暗号化
            const string plainText = @"https://qiita.com/Yametaro/items/36493c107053ae996b47
とあるWeb制作会社にて

ワイ「社長、こないだ頼まれたショッピングサイトの件なんですけど」
ワイ「ご注文フォームのコーディング、完了しましたで！」

社長「おお、ありがとうな」

ワイ「ほな、飲みに行ってきますわ！」

社長「・・・いや待てや（まだ15時やし）」
社長「何やこのフォーム」

ワイ「何ですかいな」
ワイ「デザイン通り、完璧にコーディングできてますやん」

社長「いやバリデーションが全くされとらへんがな」

ワイ「グラデュエーション？」
ワイ「何を卒業するんでっか」

社長「バリデーションや」
社長「貴様をこの会社から卒業させたろか」
";

            string encryptedText;
            using (var ms = new MemoryStream())
            using (var encryptor = csp.CreateEncryptor())
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write)) {
                using (var w = new StreamWriter(cs)) {
                    w.Write(plainText);
                }
                encryptedText = Convert.ToBase64String(ms.ToArray());
                Debug.WriteLine($"EncryptedText: [{encryptedText}]");
            }

            // 共通鍵で復号化
            using (var ms = new MemoryStream(Convert.FromBase64String(encryptedText)))
            using (var decryptor = csp.CreateDecryptor())
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var r = new StreamReader(cs)) {
                Debug.WriteLine($"DecryptedText: [{r.ReadToEnd()}]");
            }
        }

        /// <summary>
        /// RSA方式の公開鍵・秘密鍵を使った暗号化・復号化を試してみる
        /// 
        /// C# ではないがRSAの一般論として...
        ///   https://hondou.homedns.org/pukiwiki/index.php?JavaSE%20RSA%B0%C5%B9%E6
        ///   1024bit(=128byte)の鍵長であればパディングの11byteを除くと、実質的な平文の最大長は117byteとなるため、そんなに長い文字列を暗号化できるわけではない
        ///   長文を暗号化する場合、共通鍵で暗号化し、その共通鍵をRSAで暗号化してやり取りするのが一般的とされている
        /// </summary>
        private void encryptAndDecryptByAsymmetrical() {
            const string plainText = @"Hello World";

            RSAParameters publicParameters, privateParameters;
            string encryptedText;

            using (var rsa = new RSACryptoServiceProvider(1024)) {
                // 公開鍵と秘密鍵を自動生成
                publicParameters = rsa.ExportParameters(false);
                privateParameters = rsa.ExportParameters(true);
                Debug.WriteLine($"RSA PublicKey={rsa.ToXmlString(false)}");
                Debug.WriteLine($"RSA PrivateKey={rsa.ToXmlString(true)}");
            }

            // 公開鍵で暗号化
            using (var rsa = new RSACryptoServiceProvider(1024)) {
                rsa.ImportParameters(publicParameters);
                var encryptedData = rsa.Encrypt(Encoding.UTF8.GetBytes(plainText), false);
                encryptedText = Convert.ToBase64String(encryptedData);
                Debug.WriteLine($"Encrypt by RSA: EncryptedText=[{encryptedText}]");
            }

            // 秘密鍵で復号化
            using (var rsa = new RSACryptoServiceProvider(1024)) {
                rsa.ImportParameters(privateParameters);
                var decryptedData = rsa.Decrypt(Convert.FromBase64String(encryptedText), false);
                var decryptedText = Encoding.UTF8.GetString(decryptedData);
                Debug.WriteLine($"Decrypt by RSA: DecryptedText=[{decryptedText}]");
            }
        }

    }
}
