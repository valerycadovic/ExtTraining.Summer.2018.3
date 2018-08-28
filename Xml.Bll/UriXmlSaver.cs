namespace Xml.Bll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.IO;
    using System.Xml.Linq;
    using DataExportLib;

    /// <summary>
    /// XML Serializator for URL
    /// </summary>
    /// <seealso cref="DataExportLib.IDataSaver{Uri}" />
    public class UriXmlSaver : IDataSaver<Uri>
    {
        /// <summary>
        /// The stream
        /// </summary>
        private readonly Stream stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="UriXmlSaver"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <exception cref="System.ArgumentNullException">Throws if stream is null</exception>
        public UriXmlSaver(Stream stream)
        {
            this.stream = stream ?? throw new ArgumentNullException($"{nameof(stream)} is null");
        }

        /// <summary>
        /// Saves the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <exception cref="System.ArgumentNullException">Throws if data is null</exception>
        public void Save(IEnumerable<Uri> data)
        {
            if (data == null)
            {
                throw new ArgumentNullException($"{nameof(data)} is null");
            }

            var document = new XDocument(
                new XElement("urlAdresses",
                from uri in data

                // all parameters as key-value pair collection
                let parameters = 
                    uri.GetParameters()

                // valid segments
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
