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
    public class ObjectStore
    {
        private SQLiteConnection connection;

        private IObject obj;
        private DateTime time;

        private List<IStore> attributes;

        public ObjectStore(SQLiteConnection c, IObject o)
        {
            this.connection = c;
            this.obj = o;
            this.time = DateTime.Now;

            this.attributes = new List<IStore>();

            initialize();
        }

        private async void initialize()
        {
            try
            {
                SQLiteCommand i = new SQLiteCommand(this.connection);
                i.CommandText = "INSERT INTO identifiers (name, type) VALUES (@name, @type)";

                i.Parameters.Add(new SQLiteParameter("@name", this.obj.GetType().Name + "_" + this.time.Ticks));
                i.Parameters.Add(new SQLiteParameter("@type", this.obj.GetType().Name));

                await i.ExecuteNonQueryAsync();

                Type attribute = typeof(AttributeStore<>);

                foreach (IAttribute iattr in this.obj.Attributes)
                {
                    Type type = iattr.GetType();

                    Type construct = attribute.MakeGenericType(iattr.AttributeType);

                    object[] p = { this.connection, iattr, this.obj.Identifier, this.time};

                    this.attributes.Add((IStore)Activator.CreateInstance(construct, p));
                }
            }
            catch(Exception ex)
            {

            }
        }        
    }
}
