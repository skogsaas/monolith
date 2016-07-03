using Monolith.Plugins;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Plugins.SQLiteStorage
{
    public class SQLiteStorage : PluginBase
    {
        private SQLiteConnection connection;
        private Dictionary<string, ChannelStore> channels;

        public SQLiteStorage()
            : base(typeof(SQLiteStorage).Name)
        {
            this.connection = new SQLiteConnection("Data Source=" + Directory.GetCurrentDirectory() + @"\SQLiteStorage.db;Version=3;");
            this.connection.Open();

            this.channels = new Dictionary<string, ChannelStore>();

            this.channels["Signals"] = new ChannelStore(this.connection, "Signals");
            this.channels["Logging"] = new ChannelStore(this.connection, "Logging");
        }
    }
}
