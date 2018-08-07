namespace No7.Solution
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using DataExportLib;

    public class TradeFileReader : IStorageReader<string>
    {
        private readonly Stream stream;

        public TradeFileReader(Stream stream)
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
