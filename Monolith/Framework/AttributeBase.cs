using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public class AttributeBase<T> : IAttribute
    {
        protected T _value;

        public T Value
        {
            get
            {
                return this._value;
            }

            set
            {
                this.AttributeChanging?.Invoke(this);
                this._value = value;
                this.AttributeChanged?.Invoke(this);
            }
        }

        public string Name { get; }
        public Type AttributeType { get { return typeof(T); } }

        public event AttributeEventHandler AttributeChanging;
        public event AttributeEventHandler AttributeChanged;

        public AttributeBase(IContainer container, string name)
        {
            this.Name = name;
            container.add(this);
        }

        public static implicit operator T(AttributeBase<T> a)
        {
            return a.Value;
        }
    }
}
