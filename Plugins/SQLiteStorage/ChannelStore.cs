using Monolith.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Plugins.SQLiteStorage
{
    public class ChannelStore
    {
        private DbHelper db;
        private Channel channel;

        private Dictionary<string, ObjectStore> objects;

        public ChannelStore(DbHelper h, string name)
        {
            this.db = h;
            this.objects = new Dictionary<string, ObjectStore>();

            this.channel = Manager.Instance.create(name);
            this.channel.subscribe(typeof(IObject), this.onObject);
        }

        private void onObject(Channel c, IObject o)
        {
            this.objects[o.Identifier] = new ObjectStore(this.db, o);
        }
    }
}
