using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public class Manager
    {
        private static Manager manager = null;

        public static Manager Instance
        {
            get {
                if (manager == null)
                {
                    manager = new Manager();
                }

                return manager;
            }
        }

        private Dictionary<string, Channel> channels;

        public Manager()
        {
            this.channels = new Dictionary<string, Channel>();
        }

        public Channel create(string name)
        {
            if(!this.channels.ContainsKey(name))
            {
                this.channels[name] = new Channel(name);
            }

            return this.channels[name];
        }
    }
}
