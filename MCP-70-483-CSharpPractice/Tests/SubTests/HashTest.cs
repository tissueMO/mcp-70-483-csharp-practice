using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    public class HashTest : IRunnable {
        
        public void Run() {
            const string plainText = @"Agenda";
            const string compareNGText = @"@genda";

            using (var sha256 = new SHA256CryptoServiceProvider()) {
                var sha256Hash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(plainText)));
                Debug.WriteLine($"Sha256Hash: {plainText} -> {sha256Hash}");

                var compareNGSHA256Hash = Convert.ToBase64String(sha256.ComputeHash(Encoding.UTF8.GetBytes(compareNGText)));
                Debug.WriteLine($"Sha256Hash: {compareNGText} -> {compareNGSHA256Hash}");

                if (sha256Hash != compareNGSHA256Hash) {
                    Debug.WriteLine($"Sha256Hash: {plainText} : {compareNGText} -> 一致せず");
                } else {
                    Debug.WriteLine($"Sha256Hash: {plainText} : {compareNGText} -> 一致");
                }
            }
        }

    }
}
