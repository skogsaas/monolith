using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public class ObjectReference<T>
    {
        private T obj;

        public Channel Channel { get; private set; }
        public string Identifier { get; private set; }

        public delegate void ReferenceEventHandler(ObjectReference<T> r);

        public event ReferenceEventHandler ReferenceChanged;
        public event ReferenceEventHandler ReferenceChanging;

        public ObjectReference(Channel channel, string identifier)
        {
            this.Channel = channel;
            this.Identifier = identifier;

            if(Get() == null)
            {
                this.Channel.subscribePublish(typeof(T), onObjectPublish);
            }
        }

        public T Get()
        {
            if (this.obj == null)
            {
                this.obj = (T)this.Channel.find(this.Identifier);
            }

            return this.obj;
        }

        private void onObjectPublish(Channel c, IObject o)
        {
            if(this.obj == null && o.Identifier == this.Identifier)
            {
                this.ReferenceChanging?.Invoke(this);
                this.obj = (T)o;
                this.ReferenceChanged?.Invoke(this);
            }
        }
    }
}
