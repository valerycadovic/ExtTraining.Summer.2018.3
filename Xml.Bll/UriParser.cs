namespace Xml.Bll
{
    using System;
    using System.Collections.Generic;
    using DataExportLib;

    /// <summary>
    /// Parses the sequence of URLs as strings to <see cref="Uri"/> instances
    /// </summary>
    /// <seealso cref="DataExportLib.IParser{string, Uri}" />
    public class UriParser : IParser<string, Uri>
    {
        /// <summary>
        /// The validators
        /// </summary>
        private readonly IEnumerable<IValidator<string>> validators;

        /// <summary>
        /// Initializes a new instance of the <see cref="UriParser"/> class.
        /// </summary>
        /// <param name="validators">The validators.</param>
        /// <exception cref="System.ArgumentNullException">Throws when validators is null</exception>
        public UriParser(IEnumerable<IValidator<string>> validators)
        {
            this.validators = validators ?? throw new ArgumentNullException($"{nameof(validators)} is null");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UriParser"/> class.
        /// </summary>
        /// <param name="validator">The validator.</param>
        /// <exception cref="System.ArgumentNullException">throws when validator is null</exception>
        public UriParser(IValidator<string> validator)
        {
            if (validator == null)
            {
                throw new ArgumentNullException($"{nameof(validator)} is null");
            }

            this.validators = new List<IValidator<string>> {validator};
        }

        /// <summary>
        /// Parses the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>source as <see cref="Uri"/></returns>
        public Uri Parse(string source)
        {
            this.Validate(source);
            return new Uri(source);
        }

        /// <summary>
        /// Validates the specified source.
        /// </summary>
        /// <param name="source">The source.</param>
        private void Validate(string source)
        {
            foreach (var validator in validators)
            {
                validator.Validate(source);
            }
        }
    }
}