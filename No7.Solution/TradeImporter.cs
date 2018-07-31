using System;

namespace No7.Solution
{
    // посредник между записывающим и читающим устройствами
    public class TradeImporter<T>
    {
        private readonly IDataSaver<T> saver;

        private readonly AbstractStorageReader<T> reader;
        
        public TradeImporter(
            IDataSaver<T> saver,
            AbstractStorageReader<T> reader)
        {
            this.saver = saver ?? throw new ArgumentNullException($"{nameof(saver)} is null");
            this.reader = reader ?? throw new ArgumentNullException($"{nameof(reader)} is null");
        }
     
        public void Import()
        {
            T[] data = reader.Read();
            saver.Save(data);
        }
    }
}
