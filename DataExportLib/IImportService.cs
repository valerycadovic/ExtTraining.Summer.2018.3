using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataExportLib
{
    public interface IImportService<TSource, TResult>
    {
        void Import(IStorageReader<TSource> reader, IParser<TSource, TResult> parser, IDataSaver<TResult> saver);
    }
}
