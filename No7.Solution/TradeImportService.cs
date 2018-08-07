namespace DataExportLib
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using No7.Solution;

    public sealed class TradeImportService : IImportService<string, TradeRecord>
    {
        private static readonly Lazy<TradeImportService> singleton;

        static TradeImportService()
        {
            singleton = new Lazy<TradeImportService>(() => new TradeImportService());
        }

        private TradeImportService()
        {
        }

        public static TradeImportService Instance => singleton.Value;

        public void Import(IStorageReader<string> reader, IParser<string, TradeRecord> parser, IDataSaver<TradeRecord> saver)
        {
            this.Import(reader, parser, saver, null);
        }

        public void Import(IStorageReader<string> reader, IParser<string, TradeRecord> parser, IDataSaver<TradeRecord> saver,
            ILoger loger)
        {
            ValidateOnNull(reader, nameof(reader));
            ValidateOnNull(parser, nameof(parser));
            ValidateOnNull(saver, nameof(saver));

            var data = reader.Read().ToList();

            List<TradeRecord> records = new List<TradeRecord>();
            for (int i = 0; i < data.Count; i++)
            {
                try
                {
                    records.Add(parser.Parse(data[i]));
                }
                catch (Exception e) //TODO: ValidationException (custom)
                {
                    loger?.Warn($"{e.Message} {{line #{i}}}");
                }
            }

            saver.Save(records);

            loger?.Info("Success");
        }

        private static void ValidateOnNull<T>(T obj, string name) where T : class
        {
            if (obj is null)
            {
                throw new ArgumentNullException($"{name} is null");
            }
        }
    }
}
