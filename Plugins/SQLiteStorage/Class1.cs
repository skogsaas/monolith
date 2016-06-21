using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Monolith.Framework;
using Monolith.Signals;
using System.Reflection;

namespace Monolith.Plugins.SQLiteStorage
{
    public class SignalsStorage
    {
        private Channel channel;

        public SignalsStorage()
        {
            this.channel = Manager.Instance.create("Signals");
            this.channel.subscribe(typeof(IObject), this.newObject);
        }

        private void newObject(Channel c, IObject obj)
        {
            foreach(PropertyInfo p in obj.GetType().GetProperties())
            {
                if(p.CanRead && p.CanWrite)
            }
        }
    }
}
