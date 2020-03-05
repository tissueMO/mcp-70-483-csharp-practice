using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MCP_70_483_CSharpPractice.Tests.SubTests {

    /// <summary>
    /// HTTP経由のデータ取得、操作を試してみる
    /// </summary>
    public class HTTPRequestTest : IRunnable {

        public async void Run() {
            using (var client = new HttpClient()) {
                // GET してみる
                var responseFetch = await client.GetAsync(@"https://tsownserver.ddns.net/toilet-server/fetch/status");
                Debug.WriteLine($"Fetch/Status: StatusCode={responseFetch.StatusCode.ToString("D")} ({responseFetch.StatusCode.ToString("F")})");
                // これでもOK
                // Debug.WriteLine($"Fetch/Status: StatusCode={responseFetch.StatusCode.ToString("D")} ({responseFetch.ReaseonPhrase()})");
                Debug.WriteLine($"Fetch/Status: Response={Regex.Unescape(responseFetch.Content.ReadAsStringAsync().Result)}");

                // POST してみる
                var responseAction1 = await client.PostAsync(
                    @"https://tsownserver.ddns.net/toilet-server/action/close/21",
                    new StringContent(@"", Encoding.UTF8, "application/json")
                );
                Debug.WriteLine($"Action/Close: StatusCode={responseAction1.StatusCode.ToString("D")} ({responseAction1.StatusCode.ToString("F")})");
                Debug.WriteLine($"Action/Close: Response={Regex.Unescape(responseAction1.Content.ReadAsStringAsync().Result)}");

                System.Threading.Thread.Sleep(5000);

                // POST してみる
                var responseAction2 = await client.PostAsync(
                    @"https://tsownserver.ddns.net/toilet-server/action/open/21",
                    new StringContent(@"", Encoding.UTF8, "application/json")
                );
                Debug.WriteLine($"Action/Open: StatusCode={responseAction2.StatusCode.ToString("D")} ({responseAction2.StatusCode.ToString("F")})");
                Debug.WriteLine($"Action/Open: Response={Regex.Unescape(responseAction2.Content.ReadAsStringAsync().Result)}");
            }
        }

    }
}
