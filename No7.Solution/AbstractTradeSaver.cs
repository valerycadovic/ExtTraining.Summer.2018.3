using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No7.Solution
{
    public abstract class AbstractTradeSaver : IDataSaver<TradeRecord>
    {
        public void Save(TradeRecord[] data)
        {
            SaveToStorage(data);
        }

        protected abstract void SaveToStorage(TradeRecord[] data);
    }
}
