using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Configuration
{
    public class ConfigurationBase : Framework.ObjectBase
    {
        public Framework.String Plugin { get; private set; }

        public ConfigurationBase(string identifier)
            : base(identifier)
        {
            this.Plugin = new Framework.String(this, "Plugin");
            this.Plugin.Value = this.GetType().Namespace;
        }
    }
}
