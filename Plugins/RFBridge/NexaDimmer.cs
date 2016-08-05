using Monolith.Framework;
using Monolith.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monolith.Devices;

namespace RFBridge
{
    class NexaDimmer : DeviceBase
    {
        public string Name { get; set; }
        public uint Group { get; set; }
        public byte Device { get; set; }

        public Signal<int> Signal { get; private set; }

        private RadioFrequencyBridge plugin;

        public NexaDimmer(RadioFrequencyBridge p, Configuration.NexaConfig config)
            : base(typeof(RadioFrequencyBridge).Name + "." + config.Name)
        {
            this.Name = config.Name;
            this.Group = config.Group;
            this.Device = config.Device;

            this.plugin = p;

            string name = typeof(RadioFrequencyBridge).Name + "." + this.Name;

            this.Signal = new Signal<int>(name);
            this.Signal.State.AttributeChanged += this.signalChanged;

            this.plugin.SignalChannel.publish(this.Signal);
        }

        private void signalChanged(IAttribute a)
        {
            this.plugin.Transmitter.nexaDeviceDim(this.Group, this.Device, (byte)this.Signal.State);
        }
    }
}
