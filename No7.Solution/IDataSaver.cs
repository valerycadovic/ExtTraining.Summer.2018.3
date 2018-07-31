using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No7.Solution
{
    public interface IDataSaver<in T>
    {
        void Save(T[] data);
    }
}
