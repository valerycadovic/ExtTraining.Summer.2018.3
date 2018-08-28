namespace Xml.Bll
{
    using System;
    using System.Collections.Generic;
    using DataExportLib;

    public class UriParser : IParser<string, Uri>
    {
        private readonly IEnumerable<IValidator<string>> validators;

        public UriParser(IEnumerable<IValidator<string>> validators)
        {
            this.validators = validators ?? throw new ArgumentNullException($"{nameof(validators)} is null");
        }

        public Uri Parse(string source)
        {
            this.Validate(source);
            return new Uri(source);
        }

        private void Validate(string source)
        {
            foreach (var validator in validators)
            {
                validator.Validate(source);
            }
        }
    }
}