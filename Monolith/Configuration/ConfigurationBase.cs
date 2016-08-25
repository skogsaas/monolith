using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Configuration
{
    public class ConfigurationBase : Framework.ObjectBase, IConfiguration
    {
        public string Plugin { get; set; }

        public ConfigurationBase(string identifier)
            : base(identifier)
        {
            this.Plugin = this.GetType().Namespace;
        }
    }
}
