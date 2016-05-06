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
        private DeviceManager devices;

        public Server()
        {
            this.plugins = new PluginManager();
            this.devices = new DeviceManager();
        }
    }
}
