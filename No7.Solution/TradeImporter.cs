using System;

namespace No7.Solution
{
    // посредник между записывающим и читающим устройствами
    public class TradeImporter<T>
    {
        /// <summary>
        /// Может сохранять данные в любом хранилище
        /// </summary>
        private readonly IDataSaver<T> saver;

        /// <summary>
        /// Может читать данные из любого хранилища
        /// </summary>
        private readonly AbstractStorageReader<T> reader;
        
        public TradeImporter(
            IDataSaver<T> saver,
            AbstractStorageReader<T> reader)
        {
            this.saver = saver ?? throw new ArgumentNullException($"{nameof(saver)} is null");
            this.reader = reader ?? throw new ArgumentNullException($"{nameof(reader)} is null");
        }

        /// <summary>
        /// Импортирует данные из одного хранилища в другое
        /// </summary>
        public void Import()
        {
            T[] data = reader.Read();
            saver.Save(data);
        }
    }
}
