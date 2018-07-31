using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No7.Solution
{
    public interface ILoger
    {
        void Warn(string message);
        void Error(string message);
        void Fatal(string message);
        void Debug(string message);
        void Info(string message);
    }
}
