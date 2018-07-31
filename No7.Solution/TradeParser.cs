using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No7.Solution
{
    public class TradeParser : IParser<TradeRecord>
    {
        private static readonly float LotSize;

        private static readonly int NotesInLine;

        private static readonly int CharsInCurrency;

        private static readonly int OneCurrencyLingth;

        private static readonly string Delimeter;

        private readonly ILoger loger;

        static TradeParser()
        {
            try
            {
                NotesInLine = int.Parse(ConfigurationManager.AppSettings["NotesInLine"]);
                LotSize = float.Parse(ConfigurationManager.AppSettings["LotSize"]);
                CharsInCurrency = int.Parse(ConfigurationManager.AppSettings["CharsInCurrency"]);
                OneCurrencyLingth = int.Parse(ConfigurationManager.AppSettings["OneCurrencyLength"]);
            }
            catch (Exception e)
            {
                LotSize = 100000f;
                NotesInLine = 3;
                CharsInCurrency = 6;
                OneCurrencyLingth = 3;
            }
        }

        public TradeParser(ILoger loger)
        {
            this.loger = loger;
        }
        
        // надо разделить, но не хватило времени вникнуть в логику 
        public TradeRecord[] Parse(string[] lines)
        {
            var trades = new List<TradeRecord>();
            var lineCount = 1;

            foreach (var line in lines)
            {
                var fields = line.Split(Delimeter.ToCharArray());

                // 
                if (fields.Length != NotesInLine)
                {
                    loger.Warn($"WARN: Line {lineCount} malformed. Only {fields.Length} field(s) found.");
                    continue;
                }
                
                if (fields[(int)Values.Currencies].Length != CharsInCurrency)
                {
                    loger.Warn($"WARN: Trade currencies on line {lineCount} malformed: '{fields[0]}'");
                    continue;
                }

                if (!int.TryParse(fields[(int)Values.TradeAmount], out var tradeAmount))
                {
                    loger.Warn($"WARN: Trade amount on line {lineCount} not a valid integer: '{fields[1]}'");
                }

                if (!decimal.TryParse(fields[(int)Values.TradePrice], NumberStyles.Any, CultureInfo.InvariantCulture, out var tradePrice))
                {
                    loger.Warn($"WARN: Trade price on line {lineCount} not a valid decimal: '{fields[2]}'");
                }

                var sourceCurrencyCode = fields[(int)Values.Currencies].Substring(0, OneCurrencyLingth);
                var destinationCurrencyCode = fields[(int)Values.Currencies].Substring(OneCurrencyLingth, OneCurrencyLingth);

                var trade = new TradeRecord
                {
                    SourceCurrency = sourceCurrencyCode,
                    DestinationCurrency = destinationCurrencyCode,
                    Lots = tradeAmount / LotSize,
                    Price = tradePrice
                };

                trades.Add(trade);

                lineCount++;
            }

            return trades.ToArray();
        }

        private enum Values
        {
            Currencies,
            TradeAmount,
            TradePrice
        }
    }
}