using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public class ObjectBase : IObject, IContainer
    {
        private List<IAttribute> attributes;

        public string Identifier { get; private set; }

        public event ObjectEventHandler ObjectChanged;

        public ObjectBase(string identifier)
        {
            this.Identifier = identifier;
        }

        private ObjectBase()
        {
        }

        public void add(IAttribute a)
        {
            this.attributes.Add(a);
        }
    }
}