using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public delegate void AttributeCollectionEventHandler(IAttributeCollection collection, IAttribute attr);

    public interface IAttributeCollection : IAttribute, IAttributeContainer, IEnumerable<KeyValuePair<string, IAttribute>>
    {
        IAttribute this[string key] { get; }

        void Add(string key, IAttribute attr);

        void Remove(string key);

        bool ContainsKey(string key);

        void Clear();

        event AttributeCollectionEventHandler AttributeAdded;
        event AttributeCollectionEventHandler AttributeRemoved;
    }
}
