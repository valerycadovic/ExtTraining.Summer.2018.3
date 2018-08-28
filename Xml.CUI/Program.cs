using System;
using System.Configuration;
using System.IO;
using Xml.Bll;
using UriParser = Xml.Bll.UriParser;

namespace Xml.CUI
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Stream uriStream = File.OpenRead(ConfigurationManager.AppSettings["urlPath"]))
            {
                using (Stream xmlStream = new FileStream(ConfigurationManager.AppSettings["xmlPath"], 
                    FileMode.Truncate | FileMode.OpenOrCreate))
                {
                    UriImportService service = UriImportService.Instance;
                    service.Import(
                        new UriFileReader(uriStream),
                        new UriParser(
                            new[] { new UriValidator() }),
                        new UriXmlSaver(xmlStream)
                        );
                }
            }

            Console.WriteLine("done");
            Console.ReadKey();
        }
    }
}
