using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public interface IAttributeCollectionBase<T> : IAttributeCollection, IEnumerable<KeyValuePair<string, T>>
        where T : IAttribute
    {
        T this[string key] { get; }

        void Add(string key, T attr);

        void Remove(string key);

        bool ContainsKey(string key);

        void Clear();
    }
}
