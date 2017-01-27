using Skogsaas.Monolith.Plugins;
using Skogsaas.Monolith.Configuration;
using Skogsaas.Monolith.Bindings;

namespace Skogsaas.Monolith
{
    public class Server
    {
        private ConfigurationManager configs;
        private BindingsManager bindings;
        private PluginManager plugins;

        public Server()
        {
            this.configs = new ConfigurationManager();
            this.plugins = new PluginManager();
            this.bindings = new BindingsManager();
        }
    }
}
