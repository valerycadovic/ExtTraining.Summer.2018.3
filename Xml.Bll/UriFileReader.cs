namespace Xml.Bll
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using DataExportLib;

    /// <summary>
    /// Reads data from the specified <see cref="Stream"/> 
    /// </summary>
    /// <seealso cref="DataExportLib.IStorageReader{string}" />
    public class UriFileReader : IStorageReader<string>
    {
        /// <summary>
        /// The stream
        /// </summary>
        private readonly Stream stream;

        /// <summary>
        /// Initializes a new instance of the <see cref="UriFileReader"/> class.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <exception cref="System.ArgumentNullException">Throws when stream is null</exception>
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
