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

        public string Type { get { return this.GetType().ToString(); } }

        public event ObjectEventHandler ObjectChanged;

        public ObjectBase(string identifier)
        {
            this.attributes = new List<IAttribute>();
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