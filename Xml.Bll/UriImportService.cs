namespace Xml.Bll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataExportLib;

    /// <summary>
    /// The service for importing URLs from the text file to XML
    /// </summary>
    /// <seealso cref="DataExportLib.IImportService{string, Uri}" />
    public class UriImportService : IImportService<string, Uri>
    {
        #region Singleton pattern implementation
        /// <summary>
        /// The lazy instance
        /// </summary>
        private static readonly Lazy<UriImportService> LazyInstance;

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <value>
        /// The instance.
        /// </value>
        public static UriImportService Instance => LazyInstance.Value;

        /// <summary>
        /// Initializes the <see cref="UriImportService"/> class.
        /// </summary>
        static UriImportService()
        {
            LazyInstance = new Lazy<UriImportService>(() => new UriImportService());
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="UriImportService"/> class from being created.
        /// </summary>
        private UriImportService()
        {
        }
        #endregion

        #region IImportService implementation
        /// <summary>
        /// Imports data from the specified storage to another storage
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="parser">The parser.</param>
        /// <param name="saver">The saver.</param>
        public void Import(IStorageReader<string> reader, IParser<string, Uri> parser, IDataSaver<Uri> saver)
        {
            this.Import(reader, parser, saver, null);
        }

        /// <summary>
        /// Imports data from the specified storage to another storage
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="parser">The parser.</param>
        /// <param name="saver">The saver.</param>
        /// <param name="logger">The logger.</param>
        public void Import(IStorageReader<string> reader, IParser<string, Uri> parser, IDataSaver<Uri> saver, ILogger logger)
        {
            var data = reader.Read().ToList();

            var records = new List<Uri>();

            for (int i = 0; i < data.Count; i++)
            {
                try
                {
                    records.Add(parser.Parse(data[i]));
                }
                catch (Exception e) //TODO: ValidationException (custom)
                {
                    logger?.Warn($"{e.Message} {{line #{i}}}");
                }
            }

            saver.Save(records);
            logger?.Info("Success");
        }
        #endregion
    }
}
