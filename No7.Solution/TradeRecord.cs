using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No7.Solution
{
    public class TradeRecord
    {
        public string DestinationCurrency { get; set; }

        public float Lots { get; set; }

        public decimal Price { get; set; }

        public string SourceCurrency { get; set; }
    }
}
