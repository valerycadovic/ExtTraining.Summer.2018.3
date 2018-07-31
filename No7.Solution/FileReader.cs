using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No7.Solution
{
    public class FileReader<T> : AbstractStorageReader<T>
    {
        private readonly Stream stream;

        public FileReader(IParser<T> parser, Stream stream) : base(parser)
        {
            this.stream = stream;
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
