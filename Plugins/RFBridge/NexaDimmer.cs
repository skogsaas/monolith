using Monolith.Framework;
using Monolith.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFBridge
{
    class NexaDimmer
    {
        public string Name { get; set; }
        public ulong Group { get; set; }
        public uint Device { get; set; }

        public ISignal Signal { get; private set; }

        private RadioFrequencyBridge plugin;

        public NexaDimmer(RadioFrequencyBridge p, Configuration.NexaConfig config)
        {
            this.Name = config.Name;
            this.Group = config.Group;
            this.Device = config.Device;

            this.plugin = p;

            string name = typeof(RadioFrequencyBridge).Name + "." + this.Name;

            this.Signal = new Signal<int>(name);
            this.plugin.SignalChannel.publish((IObject)this.Signal);
        }
    }
}
