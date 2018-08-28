namespace Xml.Bll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using DataExportLib;

    public class UriImportService : IImportService<string, Uri>
    {
        private static readonly Lazy<UriImportService> LazyInstance;

        public static UriImportService Instance => LazyInstance.Value;

        static UriImportService()
        {
            LazyInstance = new Lazy<UriImportService>(() => new UriImportService());
        }

        private UriImportService()
        {
        }

        public void Import(IStorageReader<string> reader, IParser<string, Uri> parser, IDataSaver<Uri> saver)
        {
            this.Import(reader, parser, saver, null);
        }

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
    }
}
