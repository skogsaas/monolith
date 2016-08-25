using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public class ObjectReferenceAttribute<T> : ObjectReference<T>, IAttribute
    {
        private T obj;

        public string Name { get; private set; }

        public event AttributeEventHandler AttributeChanged;
        public event AttributeEventHandler AttributeChanging;

        public Type GetAttributeType()
        {
            return this.GetType();
        }

        public ObjectReferenceAttribute(IAttributeContainer container, string name, Channel channel, string identifier)
            : base(channel, identifier)
        {
            this.Name = name;

            container.addAttribute(this);
        }

        private void onChanging(ObjectReference<T> r)
        {
            this.AttributeChanging?.Invoke(this);
        }

        private void onChanged(ObjectReference<T> r)
        {
            this.AttributeChanged?.Invoke(this);
        }
    }
}
