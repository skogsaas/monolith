using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Monolith.Framework;
using System.Reflection;

namespace Monolith.Plugins.SQLiteStorage
{
    public class EventStore
    {
        private SQLiteConnection connection;

        private IEvent evt;
        private DateTime time;

        public EventStore(SQLiteConnection c, IEvent e)
        {
            this.connection = c;
            this.evt = e;
            this.time = DateTime.Now;

            this.initialize();
        }

        private async void initialize()
        {
            try
            {
                SQLiteCommand i = new SQLiteCommand(this.connection);
                i.CommandText = "INSERT INTO identifiers (name, type) VALUES (@name, @type)";

                i.Parameters.Add(new SQLiteParameter("@name", this.evt.GetType().Name + "_" + this.time.Ticks));
                i.Parameters.Add(new SQLiteParameter("@type", this.evt.GetType().Name));

                await i.ExecuteNonQueryAsync();

                foreach (PropertyInfo property in this.evt.GetType().GetProperties())
                {
                    SQLiteCommand p = new SQLiteCommand(this.connection);
                    p.CommandText = "INSERT INTO properties (identifier, name, value, time) VALUES (@identifier, @name, @value, @time)";

                    p.Parameters.Add(new SQLiteParameter("@identifier", this.evt.GetType().Name + "_" + this.time.Ticks));
                    p.Parameters.Add(new SQLiteParameter("@name", property.Name));
                    p.Parameters.Add(new SQLiteParameter("@value", property.GetValue(this.evt)));
                    p.Parameters.Add(new SQLiteParameter("@time", this.time.Ticks));

                    await p.ExecuteNonQueryAsync();
                }
            }
            catch(Exception ex)
            {

            }
        }

        
    }
}
