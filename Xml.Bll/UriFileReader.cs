namespace Xml.Bll
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using DataExportLib;

    public class UriFileReader : IStorageReader<string>
    {
        private readonly Stream stream;

        public UriFileReader(Stream stream)
        {
            this.stream = stream ?? throw new ArgumentNullException($"{nameof(stream)} is null");
        }

        public IEnumerable<string> Read()
        {
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    yield return line;
                }
            }
        }
    }
}
