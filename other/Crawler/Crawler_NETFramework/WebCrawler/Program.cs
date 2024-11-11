using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace WebCrawler
{
    internal class Program
    {
        static void Main(string[] args)
        {///同步寫法

            ///要下載的 url 和指定目標資料夾和檔案名稱
            string url = ""; //
            string folderPath = @"C:\Users\User\Documents\GitHub\MyPublicWork\program\train\Crawler_NETFramework";
            string filePath = Path.Combine(folderPath, "download001.html");

            try
            {
                ///若資料夾不存在則建立
                if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);

                // 發送 HTTP 請求並下載內容
                using (WebClient client = new WebClient())
                {
                    client.DownloadFile(url, filePath);
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
