using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No7.Solution
{
    /// <summary>
    /// Парсер текстовых данных
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IParser<out T>
    {
        T[] Parse(string[] data);
    }
}
