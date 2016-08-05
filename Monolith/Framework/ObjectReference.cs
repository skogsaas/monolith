using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    class ObjectReference : IAttribute
    {
        private IObject obj;

        public string Name { get; private set; }
        public Channel Channel { get; private set; }
        public string Identifier { get; private set; }

        public bool HasObject
        {
            get
            {
                if(this.obj == null)
                {
                    this.obj = this.Channel.find(this.Identifier);
                }

                return (this.obj == null);
            }
        }

        public IObject Reference
        {
            get
            {
                if(HasObject)
                {
                    return this.obj;
                }

                return null;
            }
        }

        public event AttributeEventHandler AttributeChanged;
        public event AttributeEventHandler AttributeChanging;

        public Type GetAttributeType()
        {
            return this.GetType();
        }

        public ObjectReference(IAttributeContainer container, string name, Channel channel, string identifier)
        {
            this.Name = name;
            this.Channel = channel;
            this.Identifier = identifier;

            container.addAttribute(this);
        }
    }
}
