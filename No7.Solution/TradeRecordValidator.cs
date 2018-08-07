namespace No7.Solution
{
    using System;
    using System.Globalization;
    using DataExportLib;

    public class TradeRecordValidator : IValidator<string>
    {
        private const int NotesInLine = 3;
        private const int CharsInCurrency = 6;
        private const char Delimeter = ',';

        public void Validate(string source)
        {
            var fields = source.Split(Delimeter);

            if (fields.Length != NotesInLine)
            {
                throw new ArgumentException($"Only {fields.Length} field(s) found.");
            }

            if (fields[0].Length != CharsInCurrency)
            {
                throw new ArgumentException($"Trade currencies malformed: '{fields[0]}'");
            }

            if (!int.TryParse(fields[1], out _))
            {
                throw new ArgumentException($"Trade amount not a valid integer: '{fields[1]}'");
            }
            
            if (!decimal.TryParse(fields[2], NumberStyles.Any, CultureInfo.InvariantCulture, out _))
            {
                throw new ArgumentException($"Trade price not a valid decimal: '{fields[2]}'");
            }
        }
    }
}
