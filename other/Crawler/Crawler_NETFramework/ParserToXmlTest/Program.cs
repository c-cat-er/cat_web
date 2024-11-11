using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace ParserToXmlTest
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 讀取 HTML 檔案內容
            string htmlContent = File.ReadAllText(@"C:\Users\User\Documents\GitHub\MyPublicWork\program\train\Crawler_NETFramework\download001.html");

            // 使用正則表達式解析 HTML 並組建 XML
            string xmlContent = ParseHtmlToXml(htmlContent);

            // 將 XML 內容保存到檔案中
            File.WriteAllText(@"C:\Users\User\Documents\GitHub\MyPublicWork\program\train\Crawler_NETFramework\download001.xml", xmlContent);

            Console.WriteLine("HTML parsed and converted to XML successfully.");

            // 等待用戶按下任意按鍵後退出
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static string ParseHtmlToXml(string htmlContent)
        {
            // 定義用於匹配 <h2> 和 <h4> 標籤內容的正則表達式
            string h2Pattern = "<h2>(?<content>.*?)</h2>";
            string h4Pattern = "<h4>(?<content>.*?)</h4>";

            // 創建 XML 文檔
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement rootElement = xmlDoc.CreateElement("HTMLContent");
            xmlDoc.AppendChild(rootElement);

            // 使用正則表達式進行匹配並構建 XML
            MatchCollection h2Matches = Regex.Matches(htmlContent, h2Pattern);
            foreach (Match match in h2Matches)
            {
                string content = match.Groups["content"].Value;
                AddElement(xmlDoc, rootElement, "h2", content);
            }

            MatchCollection h4Matches = Regex.Matches(htmlContent, h4Pattern);
            foreach (Match match in h4Matches)
            {
                string content = match.Groups["content"].Value;
                AddElement(xmlDoc, rootElement, "h4", content);
            }

            // 將 XML 文檔轉換為字串並返回
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
            xmlDoc.WriteTo(xmlTextWriter);
            return stringWriter.ToString();
        }

        static void AddElement(XmlDocument xmlDoc, XmlElement parentElement, string elementName, string elementValue)
        {
            XmlElement element = xmlDoc.CreateElement(elementName);
            element.InnerText = elementValue;
            parentElement.AppendChild(element);
        }
    }
}
