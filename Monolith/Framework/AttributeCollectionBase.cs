using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public class AttributeCollectionBase<T> : IAttributeCollectionBase<T>
        where T : IAttribute
    {
        private Dictionary<string, T> elements;

        public string Name { get; private set; }

        public T this[string key]
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
            this.elements = new Dictionary<string, T>();

            this.Name = name;

            container.addAttribute(this);
        }

        public Dictionary<string, IAttribute> GetAttributes()
        {
            Dictionary<string, IAttribute> collection = new Dictionary<string, IAttribute>();

            foreach(KeyValuePair<string, T> pair in this.elements)
            {
                collection.Add(pair.Key, (IAttribute)pair.Value);
            }

            return collection;
        }

        public Type GetAttributeType()
        {
            return typeof(T);
        }

        public void Add(string key, T attr)
        {
            if (!this.elements.ContainsKey(attr.Name))
            {
                this.elements.Add(attr.Name, (T)attr);

                attr.AttributeChanging += onAttributeChanging;
                attr.AttributeChanged += onAttributeChanged;

                this.AttributeAdded?.Invoke(attr);
            }
        }

        public void Remove(string key)
        {
            if(this.elements.ContainsKey(key))
            {
                IAttribute attr = this.elements[key];
                attr.AttributeChanging -= onAttributeChanging;
                attr.AttributeChanged -= onAttributeChanged;

                this.elements.Remove(key);

                this.AttributeRemoved?.Invoke(attr);
            }
        }

        public bool ContainsKey(string key)
        {
            return this.elements.ContainsKey(key);
        }

        public void Clear()
        {
            foreach(KeyValuePair<string, T> pair in this.elements)
            {
                pair.Value.AttributeChanging -= onAttributeChanging;
                pair.Value.AttributeChanged -= onAttributeChanged;
            }

            this.elements.Clear();
        }

        public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
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
            if(!this.elements.ContainsKey(a.Name))
            {
                this.elements.Add(a.Name, (T)a);

                a.AttributeChanging += onAttributeChanging;
                a.AttributeChanged += onAttributeChanged;

                this.AttributeAdded?.Invoke(a);
            }
        }
    }
}
