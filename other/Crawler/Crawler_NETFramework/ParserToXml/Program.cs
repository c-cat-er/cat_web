using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace ParserToXml
{
    internal class Program
    {
        static void Main(string[] args)
        {///同步寫法 + 使用 RegEx

            ///讀取 HTML 檔案內容
            string htmlContent = File.ReadAllText(@"C:\Users\User\Documents\GitHub\MyPublicWork\program\train\Crawler_NETFramework\download001.html");

            // k-p, 使用正則表達式解析 HTML 並組建 XML
            string xmlContent = ParseHtmlToXml(htmlContent);

            // k-p, 將 XML 內容保存到檔案中
            File.WriteAllText(@"C:\Users\User\Documents\GitHub\MyPublicWork\\program\train\Crawler_NETFramework\download001.html", xmlContent);

            Console.WriteLine("HTML parsed and converted to XML successfully.");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static string ParseHtmlToXml(string htmlContent)
        {
            // k-p, 使用正則表達式匹配各個部分的內容並提取
            string pnPattern = @"<PN>(?<pn>.*?)</PN>";
            string apnPattern = @"<APN>(?<apn>.*?)</APN>";
            string apdPattern = @"<APD>(?<apd>.*?)</APD>";
            string ttlPattern = @"<TTL>(?<ttl>.*?)</TTL>";
            string namPattern = @"<NAM>(?<nam>.*?)</NAM>";
            string abstPattern = @"<PAL>(?<pal>.*?)</PAL>";

            string pn = Regex.Match(htmlContent, pnPattern).Groups["pn"].Value;
            string apn = Regex.Match(htmlContent, apnPattern).Groups["apn"].Value;
            string apd = Regex.Match(htmlContent, apdPattern).Groups["apd"].Value;
            string ttl = Regex.Match(htmlContent, ttlPattern).Groups["ttl"].Value;
            string[] namMatches = Regex.Matches(htmlContent, namPattern)
                .Cast<Match>()
                .Select(match => match.Groups["nam"].Value)
                .ToArray();
            string abst = Regex.Match(htmlContent, abstPattern).Groups["pal"].Value;

            // 創建 XML 文檔
            XmlDocument xmlDoc = new XmlDocument();
            XmlElement rootElement = xmlDoc.CreateElement("PatentInfo");
            xmlDoc.AppendChild(rootElement);

            // 添加各個部分到 XML 文檔中
            AddElement(xmlDoc, rootElement, "PN", pn);
            AddElement(xmlDoc, rootElement, "APN", apn);
            AddElement(xmlDoc, rootElement, "APD", apd);
            AddElement(xmlDoc, rootElement, "TTL", ttl);

            XmlElement invtsElement = xmlDoc.CreateElement("INVTS");
            rootElement.AppendChild(invtsElement);
            foreach (string nam in namMatches)
            {
                AddElement(xmlDoc, invtsElement, "NAM", nam);
            }

            AddElement(xmlDoc, rootElement, "ABST", abst);

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
