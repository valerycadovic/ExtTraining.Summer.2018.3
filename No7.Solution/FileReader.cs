using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No7.Solution
{
    /// <summary>
    /// Читает данные любого типа из потока
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="No7.Solution.AbstractStorageReader{T}" />
    public class FileReader<T> : AbstractStorageReader<T>
    {
        private readonly Stream stream;

        public FileReader(IParser<T> parser, Stream stream) : base(parser)
        {
            this.stream = stream ?? throw new ArgumentNullException($"{nameof(stream)} is null");
        }

        protected override string[] GetData()
        {
            var lines = new List<string>();
            using (var reader = new StreamReader(stream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            return lines.ToArray();
        }
    }
}
