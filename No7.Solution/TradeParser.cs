using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;

namespace No7.Solution
{
    /// <summary>
    /// Реализация парсера для модели TradeRecord
    /// </summary>
    /// <seealso cref="No7.Solution.IParser{No7.Solution.TradeRecord}" />
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
                Delimeter = ConfigurationManager.AppSettings["Delimeter"];
            }
            catch (Exception e)
            {
                LotSize = 100000f;
                NotesInLine = 3;
                CharsInCurrency = 6;
                OneCurrencyLingth = 3;
                Delimeter = ",";
            }
        }

        public TradeParser(ILoger loger)
        {
            this.loger = loger ?? throw new ArgumentNullException($"{loger} is null");
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
                    loger.Warn($"Line {lineCount} malformed. Only {fields.Length} field(s) found.");
                    continue;
                }
                
                if (fields[(int)Values.Currencies].Length != CharsInCurrency)
                {
                    loger.Warn($"Trade currencies on line {lineCount} malformed: '{fields[0]}'");
                    continue;
                }

                if (!int.TryParse(fields[(int)Values.TradeAmount], out var tradeAmount))
                {
                    loger.Warn($"Trade amount on line {lineCount} not a valid integer: '{fields[1]}'");
                }

                // TODO: Абстрагироваться от настроек культуры
                if (!decimal.TryParse(fields[(int)Values.TradePrice], NumberStyles.Any, CultureInfo.InvariantCulture, out var tradePrice))
                {
                    loger.Warn($"Trade price on line {lineCount} not a valid decimal: '{fields[2]}'");
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

        /// <summary>
        /// Именованные константы для парсинга
        /// </summary>
        private enum Values
        {
            Currencies,
            TradeAmount,
            TradePrice
        }
    }
}