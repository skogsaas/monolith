using Monolith.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Monolith.Plugins.SQLiteStorage
{
    public class ChannelStore
    {
        private SQLiteConnection connection;
        private Channel channel;

        private Dictionary<string, ObjectStore> objects;

        public ChannelStore(SQLiteConnection c, string name)
        {
            this.connection = c;
            this.objects = new Dictionary<string, ObjectStore>();

            this.channel = Manager.Instance.create(name);
            this.channel.subscribe(typeof(IObject), this.onObject);
            this.channel.subscribe(typeof(IEvent), this.onEvent);
        }

        private void onObject(Channel c, IObject o)
        {
            this.objects[o.Identifier] = new ObjectStore(this.connection, o);
        }

        private void onEvent(Channel c, IEvent e)
        {
            EventStore store = new EventStore(this.connection, e);
        }
    }
}
