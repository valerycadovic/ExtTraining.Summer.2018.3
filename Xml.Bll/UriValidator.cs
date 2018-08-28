namespace Xml.Bll
{
    using System;
    using System.Text.RegularExpressions;
    using DataExportLib;

    public class UriValidator : IValidator<string>
    {
        public void Validate(string source)
        {
            Regex regex = new Regex(@"(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?");

            if (!regex.IsMatch(source))
            {
                throw new UriFormatException();
            }
        }
    }
}
