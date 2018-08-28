namespace Xml.Bll
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Set of <see cref="Uri"/> extension methods
    /// </summary>
    internal static class UriHelpers
    {
        /// <summary>
        /// Gets the parameters.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>
        /// Dictionary of keys and values of the url parameters list
        /// </returns>
        /// <exception cref="System.ArgumentNullException">Throws when uri is null</exception>
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

        public static IEnumerable<string> GetClearSegments(this Uri uri) =>
            from s in uri.Segments
            select s.Replace("/", string.Empty)
                into clear
            where !string.IsNullOrEmpty(clear)
            select clear;

    }
}
