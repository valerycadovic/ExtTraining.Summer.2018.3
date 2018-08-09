using System.Globalization;

namespace No7.Solution
{
    using System;
    using System.Collections.Generic;
    using DataExportLib;

    public class TradeRecordParser : IParser<string, TradeRecord>
    {
        #region Constants
        private const float LotSize = 100000f;
        private const int OneCurrencyLength = 3;
        private const char Delimeter = ',';
        #endregion

        #region Private Fields
        private readonly IEnumerable<IValidator<string>> validators;
        #endregion

        #region Constructors
        public TradeRecordParser(IEnumerable<IValidator<string>> validators)
        {
            this.validators = validators ?? throw new ArgumentNullException($"{nameof(validators)} is null");
        }
        #endregion

        #region Public API: IParser<string, TradeRecord> implementation
        public TradeRecord Parse(string source)
        {
            Validate(source);

            string[] fields = source.Split(Delimeter);

            string sourceCurrencyCode = fields[(int)Values.Currencies].Substring(0, OneCurrencyLength);
            string destinationCurrencyCode = fields[(int)Values.Currencies].Substring(OneCurrencyLength, OneCurrencyLength);

            int tradeAmount = int.Parse(fields[(int)Values.TradeAmount]);
            decimal tradePrice = decimal.Parse(fields[(int)Values.TradePrice], NumberStyles.Any, CultureInfo.InvariantCulture);
            //decimal.TryParse(fields[(int)Values.TradePrice], NumberStyles.Any, CultureInfo.InvariantCulture, out var tradePrice)

            var trade = new TradeRecord
            {
                SourceCurrency = sourceCurrencyCode,
                DestinationCurrency = destinationCurrencyCode,
                Lots = tradeAmount / LotSize,
                Price = tradePrice
            };

            return trade;
        }
        #endregion

        #region Private validation methods
        private void Validate(string source)
        {
            foreach (var validator in validators)
            {
                validator.Validate(source);
            }
        }
        #endregion

        #region Private utility types
        private enum Values
        {
            Currencies,
            TradeAmount,
            TradePrice
        }
        #endregion
    }
}
