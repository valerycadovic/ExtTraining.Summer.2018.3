namespace Xml.Bll
{
    using System;
    using System.Text.RegularExpressions;
    using DataExportLib;

    /// <summary>
    /// URL validator class
    /// </summary>
    /// <seealso cref="DataExportLib.IValidator{string}" />
    public class UriValidator : IValidator<string>
    {
        /// <summary>
        /// Validates string on matching the URL format
        /// </summary>
        /// <param name="source">The source.</param>
        /// <exception cref="UriFormatException">source</exception>
        public void Validate(string source)
        {
            Regex regex = new Regex(@"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?");

            if (!regex.IsMatch(source))
            {
                throw new UriFormatException($"{nameof(source)} has wrong url format");
            }
        }
    }
}
