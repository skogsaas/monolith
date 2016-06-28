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
        private DbHelper db;

        private IObject obj;
        private List<IStore> attributes;

        public ObjectStore(DbHelper helper, IObject o)
        {
            this.db = helper;
            this.obj = o;
            this.attributes = new List<IStore>();

            this.store();

            initialize();
        }

        private void initialize()
        {
            Type attribute = typeof(AttributeStore<>);

            foreach(IAttribute iattr in this.obj.Attributes)
            {
                Type type = iattr.GetType();

                Type construct = attribute.MakeGenericType(iattr.AttributeType);

                object[] p = { this.db, iattr, this.obj.Identifier, iattr.Name };

                this.attributes.Add((IStore)Activator.CreateInstance(construct, p));
            }
        }

        private void store()
        {
            string sql = "INSERT OR REPLACE INTO identifiers (identifier) VALUES('" + this.obj.Identifier + "')";

            this.db.ExecuteNonQuery(sql);
        }

        
    }
}
