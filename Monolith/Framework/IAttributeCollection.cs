using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public delegate void AttributeCollectionEventHandler(IAttribute attr);

    public interface IAttributeCollection<T> : IAttribute, IAttributeContainer, IEnumerable<KeyValuePair<string, T>>
        where T : IAttribute
    {
        T this[string key] { get; }

        void Add(string key, T attr);

        void Remove(string key);

        bool ContainsKey(string key);

        void Clear();

        event AttributeCollectionEventHandler AttributeAdded;
        event AttributeCollectionEventHandler AttributeRemoved;
    }
}
