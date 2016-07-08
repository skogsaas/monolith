using Monolith.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith
{
    public class Server
    {
        private PluginManager plugins;

        public Server()
        {
            this.plugins = new PluginManager();
        }
    }
}
