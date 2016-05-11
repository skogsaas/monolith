using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public class Channel
    {
        private Dictionary<string, IObject> objects;

        private Utilities.MultiDictionary<Type, ObjectPublishedHandler> objectSubscriptions;
        private Utilities.MultiDictionary<Type, EventPublishedHandler> eventSubscriptions;

        public string Name { get; private set; }

        public delegate void ObjectPublishedHandler(Channel channel, IObject obj);
        public delegate void EventPublishedHandler(Channel channel, IEvent evt);

        public Channel(string name)
        {
            this.objects = new Dictionary<string, IObject>();
            this.objectSubscriptions = new Utilities.MultiDictionary<Type, ObjectPublishedHandler>();
            this.eventSubscriptions = new Utilities.MultiDictionary<Type, EventPublishedHandler>();

            this.Name = name;
        }

        public IObject find(string name)
        {
            if(this.objects.ContainsKey(name))
            {
                return this.objects[name];
            }

            return null;
        }

        public void publish(IObject obj)
        {
            this.objects[obj.Identifier] = obj;
            triggerObjectPublished(obj);
        }

        public void publish(IEvent evt)
        {
            triggerEventPublished(evt);
        }

        public void subscribe(Type type, ObjectPublishedHandler handler)
        {
            if(typeof(IObject).IsAssignableFrom(type))
            {
                this.objectSubscriptions[type].Add(handler);

                foreach(KeyValuePair<string, IObject> pair in this.objects)
                {
                    if(pair.Value.GetType().IsAssignableFrom(type))
                    {
                        handler.Invoke(this, pair.Value);
                    }
                }
            }
        }

        public void subscribe(Type type, EventPublishedHandler handler)
        {
            if (typeof(IEvent).IsAssignableFrom(type))
            {
                this.eventSubscriptions[type].Add(handler);

                // TODO Check for existing subtypes
            }
        }

        private void triggerObjectPublished(IObject obj)
        {
            foreach(KeyValuePair<Type, ObjectPublishedHandler> pair in this.objectSubscriptions)
            {
                if(pair.Key.IsAssignableFrom(obj.GetType()))
                {
                    pair.Value.Invoke(this, obj);
                }
            }
        }

        private void triggerEventPublished(IEvent evt)
        {
            foreach (KeyValuePair<Type, EventPublishedHandler> pair in this.eventSubscriptions)
            {
                if (pair.Key.IsAssignableFrom(evt.GetType()))
                {
                    pair.Value.Invoke(this, evt);
                }
            }
        }
    }
}
