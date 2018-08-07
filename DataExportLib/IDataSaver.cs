namespace DataExportLib
{
    using System.Collections.Generic;

    public interface IDataSaver<in T>
    {
        void Save(IEnumerable<T> data);
    }
}
