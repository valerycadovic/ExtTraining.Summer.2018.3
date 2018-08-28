namespace Xml.Bll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class UriHelpers
    {
        public static Dictionary<string, string> GetParameters(this Uri uri)
        {
            if (uri == null)
            {
                throw new ArgumentNullException($"{nameof(uri)} is null");
            }

            if (uri.Query.Length == 0)
            {
                return new Dictionary<string, string>();
            }

            var result = uri.Query.TrimStart('?')
                .Split(
                    new[] { '&', ';' },
                    StringSplitOptions.RemoveEmptyEntries)
                .Select(x =>
                    x.Split(
                        new[] { '=' },
                        StringSplitOptions.RemoveEmptyEntries
                    ))
                .GroupBy(
                    xs => xs[0],
                    xs => xs.Length > 2
                        ? string.Join("=", xs, 1, xs.Length - 1)
                        : (xs.Length > 1 ? xs[1] : string.Empty))
                .ToDictionary(g => g.Key,
                    g => string.Join(",", g));

            return result;
        }
    }
}
