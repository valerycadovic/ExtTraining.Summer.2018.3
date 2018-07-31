using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No7.Solution
{
    /// <summary>
    /// Сохраняет данные любого типа в любом хранилище
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDataSaver<in T>
    {
        void Save(T[] data);
    }
}
