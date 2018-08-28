namespace DataExportLib
{
    public interface IImportService<TSource, TResult>
    {
        void Import(IStorageReader<TSource> reader, IParser<TSource, TResult> parser, IDataSaver<TResult> saver);
    }
}
