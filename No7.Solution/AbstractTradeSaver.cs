using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No7.Solution
{
    /// <summary>
    /// Сохраняет объект TradeRecord в хранилище
    /// </summary>
    /// <seealso cref="No7.Solution.IDataSaver{No7.Solution.TradeRecord}" />
    public abstract class AbstractTradeSaver : IDataSaver<TradeRecord>
    {
        public void Save(TradeRecord[] data)
        {
            SaveToStorage(data);
        }

        protected abstract void SaveToStorage(TradeRecord[] data);
    }
}
