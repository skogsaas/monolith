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

        #region Subscriptions

        private Utilities.MultiDictionary<Type, ObjectHandler> objectPublish;
        private Utilities.MultiDictionary<Type, ObjectHandler> objectUnpublish;
        private Utilities.MultiDictionary<Type, EventHandler> eventPublish;

        #endregion

        public string Name { get; private set; }

        public delegate void ObjectHandler(Channel channel, IObject obj);
        public delegate void EventHandler(Channel channel, IEvent evt);

        public Channel(string name)
        {
            this.objects = new Dictionary<string, IObject>();
            this.objectPublish = new Utilities.MultiDictionary<Type, ObjectHandler>();
            this.objectUnpublish = new Utilities.MultiDictionary<Type, ObjectHandler>();
            this.eventPublish = new Utilities.MultiDictionary<Type, EventHandler>();

            this.Name = name;
        }

        public IObject find(string identifier)
        {
            if(this.objects.ContainsKey(identifier))
            {
                return this.objects[identifier];
            }

            return null;
        }

        public List<T> findOfType<T>()
        {
            Type type = typeof(T);

            List<T> objects = new List<T>();

            foreach (KeyValuePair<string, IObject> pair in this.objects)
            {
                if (type.IsAssignableFrom(pair.Value.GetType()))
                {
                    objects.Add((T)pair.Value);
                }
            }

            return objects;
        }

        public void publish(IObject obj)
        {
            this.objects[obj.Identifier] = obj;
            triggerObjectPublished(obj);
        }

        public void unpublish(IObject obj)
        {
            if(this.objects.ContainsKey(obj.Identifier))
            {
                this.objects.Remove(obj.Identifier);
                triggerObjectUnpublished(obj);
            }
        }

        public void publish(IEvent evt)
        {
            triggerEventPublished(evt);
        }

        public void subscribePublish(Type type, ObjectHandler handler)
        {
            this.objectPublish[type].Add(handler);

            foreach(KeyValuePair<string, IObject> pair in this.objects)
            {
                if(type.IsAssignableFrom(pair.Value.GetType()))
                {
                    handler.Invoke(this, pair.Value);
                }
            }
        }

        public void subscribeUnpublish(Type type, ObjectHandler handler)
        {
            this.objectUnpublish[type].Add(handler);
        }

        public void subscribePublish(Type type, EventHandler handler)
        {
            this.eventPublish[type].Add(handler);
        }

        private void triggerObjectPublished(IObject obj)
        {
            foreach(KeyValuePair<Type, ObjectHandler> pair in this.objectPublish)
            {
                if(pair.Key.IsAssignableFrom(obj.GetType()))
                {
                    pair.Value.Invoke(this, obj);
                }
            }
        }

        private void triggerObjectUnpublished(IObject obj)
        {
            foreach (KeyValuePair<Type, ObjectHandler> pair in this.objectUnpublish)
            {
                if (pair.Key.IsAssignableFrom(obj.GetType()))
                {
                    pair.Value.Invoke(this, obj);
                }
            }
        }

        private void triggerEventPublished(IEvent evt)
        {
            foreach (KeyValuePair<Type, EventHandler> pair in this.eventPublish)
            {
                if (pair.Key.IsAssignableFrom(evt.GetType()))
                {
                    pair.Value.Invoke(this, evt);
                }
            }
        }
    }
}
