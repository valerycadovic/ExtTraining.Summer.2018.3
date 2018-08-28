namespace Xml.Bll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.Xml.Linq;
    using DataExportLib;

    public class UriXmlSaver : IDataSaver<Uri>
    {
        private readonly Stream stream;

        public UriXmlSaver(Stream stream)
        {
            this.stream = stream ?? throw new ArgumentNullException($"{nameof(stream)} is null");
        }

        public void Save(IEnumerable<Uri> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException($"{nameof(data)} is null");
            }

            var document = new XDocument(
                new XElement("urlAdresses",
                from uri in data

                let parameters = 
                    uri.GetParameters()

                let segments = 
                    (from s in uri.Segments
                    select s.Replace("/", string.Empty) into clear
                    where !string.IsNullOrEmpty(clear)
                    select clear).ToList()

                select
                    new XElement("urlAddress",
                        new XElement("host",
                            new XAttribute("name", uri.Host)
                            ),

                        segments.Count != 0
                        ? new XElement("uri",
                            from s in segments
                            select
                                new XElement("segment", s)
                            )
                        : null,

                        parameters.Count != 0
                        ? new XElement("parameters",
                            from couple in parameters
                            select
                            new XElement("parameter",
                                new XAttribute("value", couple.Value),
                                new XAttribute("key", couple.Key)
                                )
                            )
                        : null
                        )
                    )
                );

            document.Save(this.stream);
        }
    }
}
