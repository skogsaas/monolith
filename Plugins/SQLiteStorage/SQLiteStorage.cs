using Monolith.Plugins;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Plugins.SQLiteStorage
{
    public class SQLiteStorage : PluginBase
    {
        private DbHelper db;
        private Dictionary<string, ChannelStore> channels;

        public SQLiteStorage()
            : base(typeof(SQLiteStorage).Name)
        {
            this.db = new DbHelper(Directory.GetCurrentDirectory() + @"\SQLiteStorage.db;Version=3;");
            this.channels = new Dictionary<string, ChannelStore>();

            this.channels["Signals"] = new ChannelStore(this.db, "Signals");
        }
    }
}
