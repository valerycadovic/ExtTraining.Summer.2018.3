﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace No7.Solution
{
    /// <summary>
    /// Читает данные из хранилища и парсит в нужный тип
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class AbstractStorageReader<T>
    {
        private readonly IParser<T> parser;

        protected AbstractStorageReader(IParser<T> parser)
        {
            this.parser = parser ?? throw new ArgumentNullException($"{nameof(parser)} is null");
        }

        public T[] Read()
        {
            var data = GetData();
            return parser.Parse(data);
        }

        protected abstract string[] GetData();
    }
}
