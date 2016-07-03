using Monolith.Framework;
using Monolith.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFBridge
{
    class NexaSwitch
    {
        public string Name { get; set; }
        public uint Group { get; set; }
        public byte Device { get; set; }

        public Signal<bool> Signal { get; private set; }

        private RadioFrequencyBridge plugin;

        public NexaSwitch(RadioFrequencyBridge p, RadioFrequencyBridgeConfiguration.NexaConfig config)
        {
            this.Name = config.Name;
            this.Group = config.Group;
            this.Device = config.Device;

            this.plugin = p;

            string name = typeof(RadioFrequencyBridge).Name + "." + this.Name;

            this.Signal = new Signal<bool>(name, Signal<bool>.AllwaysAccept);
            this.Signal.InnerState.AttributeChanged += this.signalChanged;

            this.plugin.SignalChannel.publish(this.Signal);
        }

        private void signalChanged(IAttribute a)
        {
            this.plugin.Transmitter.nexaDeviceOnOff(this.Group, this.Device, this.Signal.State);
        }
    }
}
