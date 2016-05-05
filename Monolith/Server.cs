using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith
{
    public class Server
    {
        private PluginManager plugins = null;
        private DeviceManager devices = null;

        public Server()
        {
            this.plugins = new PluginManager();
            this.devices = new DeviceManager();
        }
    }
}
