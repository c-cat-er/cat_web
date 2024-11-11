using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawlerAsync
{
    internal class Program
    {
        private static readonly HttpClient client = new HttpClient();

        static async Task Main(string[] args)
        {///異步寫法

             ///要下載的 url 和指定目標資料夾和檔案名稱
            string url = ""; //
            string folderPath = @"C:\Users\User\Documents\GitHub\MyPublicWork\program\train\Crawler_NETFramework";
            string filePath = Path.Combine(folderPath, "download001.html");

            try
            {
                ///若資料夾不存在則建立
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                // 發送 HTTP 請求並下載內容
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string content = await response.Content.ReadAsStringAsync();

                using (var streamWriter = new StreamWriter(filePath, false))
                {
                    await streamWriter.WriteAsync(content);
                }

                Console.WriteLine("Web page downloaded successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while downloading the web page: " + ex.Message);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}
