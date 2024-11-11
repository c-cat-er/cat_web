using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ParserToXmlAsyncUsepack
{
    internal class Program
    {
        static async Task Main(string[] args)
        {///異步寫法 + use pack HtmlAgilityPack.

            string filePath = @"C:\Users\User\Documents\GitHub\MyPublicWork\program\train\Crawler_NETFramework\download001.html";
            string outputPath = @"C:\Users\User\Documents\GitHub\MyPublicWork\program\train\Crawler_NETFramework\download001.xml";

            try
            {
                // 使用 StreamReader 異步讀取文件
                string htmlContent;
                using (var reader = new StreamReader(filePath))
                {
                    htmlContent = await reader.ReadToEndAsync();
                }

                string xmlContent = ParseHtmlToXml(htmlContent);

                // 使用 StreamWriter 異步寫入文件
                using (var writer = new StreamWriter(outputPath, false))
                {
                    await writer.WriteAsync(xmlContent);
                }

                Console.WriteLine("HTML parsed and converted to XML successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error processing the document: " + ex.Message);
            }

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static string ParseHtmlToXml(string htmlContent)
        {
            HtmlDocument htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(htmlContent);

            XmlDocument xmlDoc = new XmlDocument();
            XmlElement rootElement = xmlDoc.CreateElement("PatentInfo");
            xmlDoc.AppendChild(rootElement);

            HtmlNode pnNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='PN']");
            HtmlNode apnNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='APN']");
            HtmlNode apdNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='APD']");
            HtmlNode ttlNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='TTL']");
            HtmlNodeCollection namNodes = htmlDoc.DocumentNode.SelectNodes("//div[@id='NAM']");
            HtmlNode abstNode = htmlDoc.DocumentNode.SelectSingleNode("//div[@id='PAL']");

            AddElement(xmlDoc, rootElement, "PN", pnNode?.InnerText);
            AddElement(xmlDoc, rootElement, "APN", apnNode?.InnerText);
            AddElement(xmlDoc, rootElement, "APD", apdNode?.InnerText);
            AddElement(xmlDoc, rootElement, "TTL", ttlNode?.InnerText);

            if (namNodes != null)
            {
                XmlElement invtsElement = xmlDoc.CreateElement("INVTS");
                rootElement.AppendChild(invtsElement);
                foreach (HtmlNode namNode in namNodes)
                {
                    AddElement(xmlDoc, invtsElement, "NAM", namNode.InnerText);
                }
            }

            AddElement(xmlDoc, rootElement, "ABST", abstNode?.InnerText);

            StringWriter stringWriter = new StringWriter();
            xmlDoc.Save(stringWriter);
            return stringWriter.ToString();
        }

        static void AddElement(XmlDocument xmlDoc, XmlElement parentElement, string elementName, string elementValue)
        {
            XmlElement element = xmlDoc.CreateElement(elementName);
            element.InnerText = elementValue ?? "Not Available";
            parentElement.AppendChild(element);
        }
    }
}
