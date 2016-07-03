using Monolith.Framework;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Plugins.SQLiteStorage
{
    class AttributeStore<T> : IStore
    {
        private SQLiteConnection connection;

        private AttributeBase<T> property;
        private string identifier;

        private DateTime time;

        public AttributeStore(SQLiteConnection c, AttributeBase<T> p, string i, DateTime t)
        {
            this.connection = c;
            this.property = p;
            this.identifier = i;
            this.time = t;

            this.property.AttributeChanged += Property_AttributeChanged;

            store();
        }

        private void Property_AttributeChanged(IAttribute a)
        {
            store();
        }

        public async void store()
        {
            try
            {
                SQLiteCommand p = new SQLiteCommand(this.connection);
                p.CommandText = "INSERT INTO properties (identifier, name, value, time) VALUES (@identifier, @name, @value, @time)";

                p.Parameters.Add(new SQLiteParameter("@identifier", this.identifier));
                p.Parameters.Add(new SQLiteParameter("@name", property.Name));
                p.Parameters.Add(new SQLiteParameter("@value", property.Value));
                p.Parameters.Add(new SQLiteParameter("@time", this.time.Ticks));

                await p.ExecuteNonQueryAsync();
            }
            catch(Exception ex)
            {

            }
        }
    }
}
