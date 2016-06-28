using Monolith.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Plugins.SQLiteStorage
{
    class AttributeStore<T> : IStore
    {
        private DbHelper db;

        private AttributeBase<T> property;
        private string identifier;
        private string name;

        public AttributeStore(DbHelper h, AttributeBase<T> p, string i, string n)
        {
            this.db = h;

            this.property = p;
            this.identifier = i;
            this.name = n;

            this.property.AttributeChanged += Property_AttributeChanged;

            store();
        }

        private void Property_AttributeChanged(IAttribute a)
        {
            store();
        }

        public void store()
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            values["identifier"] = this.identifier;
            values["name"] = this.name;
            values["value"] = property.Value.ToString();
            values["time"] = DateTime.Now.ToString();

            if (!this.db.Insert("properties", values))
            {
                Logging.Logger.Warning("Could not insert values into database!");
            }
        }
    }
}
