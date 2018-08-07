namespace DataExportLib
{
    using System.Collections.Generic;

    public interface IStorageReader<out T>
    {
        IEnumerable<T> Read();
    }
}
