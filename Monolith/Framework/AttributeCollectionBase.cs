using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public class AttributeCollectionBase : IAttributeCollection
    {
        private Dictionary<string, IAttribute> elements;

        public string Name { get; private set; }

        public IAttribute this[string key]
        {
            get
            {
                if(this.elements.ContainsKey(key))
                {
                    return this.elements[key];
                }
                else
                {
                    throw new KeyNotFoundException();
                }
            }
        }

        public event AttributeEventHandler AttributeChanged;
        public event AttributeEventHandler AttributeChanging;

        public event AttributeCollectionEventHandler AttributeAdded;
        public event AttributeCollectionEventHandler AttributeRemoved;

        public AttributeCollectionBase(IAttributeContainer container, string name)
        {
            this.elements = new Dictionary<string, IAttribute>();

            this.Name = name;

            container.addAttribute(this);
        }

        public Type GetAttributeType()
        {
            return this.GetType();
        }

        public void Add(string key, IAttribute attr)
        {
            this.elements.Add(key, attr);

            attr.AttributeChanging += onAttributeChanging;
            attr.AttributeChanged += onAttributeChanged;

            this.AttributeAdded?.Invoke(this, attr);
        }

        public void Remove(string key)
        {
            if(this.elements.ContainsKey(key))
            {
                IAttribute attr = this.elements[key];
                attr.AttributeChanging -= onAttributeChanging;
                attr.AttributeChanged -= onAttributeChanged;

                this.elements.Remove(key);

                this.AttributeRemoved?.Invoke(this, attr);
            }
        }

        public bool ContainsKey(string key)
        {
            return this.elements.ContainsKey(key);
        }

        public void Clear()
        {
            foreach(KeyValuePair<string, IAttribute> pair in this.elements)
            {
                pair.Value.AttributeChanging -= onAttributeChanging;
                pair.Value.AttributeChanged -= onAttributeChanged;
            }

            this.elements.Clear();
        }

        public IEnumerator<KeyValuePair<string, IAttribute>> GetEnumerator()
        {
            return this.elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void onAttributeChanging(IAttribute attr)
        {
            this.AttributeChanging?.Invoke(attr);
        }

        private void onAttributeChanged(IAttribute attr)
        {
            this.AttributeChanged?.Invoke(attr);
        }

        public void addAttribute(IAttribute a)
        {
            // Do nothing, as we should already know of this attribute
        }
    }
}
