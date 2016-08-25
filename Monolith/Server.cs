using Monolith.Plugins;
using Monolith.Bindings;
using Monolith.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith
{
    public class Server
    {
        private ConfigurationManager configs;
        private BindingManager bindings;
        private PluginManager plugins;

        public Server()
        {
            this.configs = new ConfigurationManager();
            this.bindings = new BindingManager();
            this.plugins = new PluginManager();
        }
    }
}
