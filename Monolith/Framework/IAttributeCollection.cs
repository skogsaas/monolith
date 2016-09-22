using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public delegate void AttributeCollectionEventHandler(IAttribute attr);

    public interface IAttributeCollection : IAttribute, IAttributeContainer
    {
        Dictionary<string, IAttribute> GetAttributes();

        event AttributeCollectionEventHandler AttributeAdded;
        event AttributeCollectionEventHandler AttributeRemoved;
    }
}
