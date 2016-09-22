using Monolith.Devices;
using Monolith.Framework;
using Monolith.Signaling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexaController
{
    class NexaSwitch : DeviceBase
    {
        public string Name { get; set; }
        public uint Group { get; set; }
        public byte Device { get; set; }

        public Slot<bool> Slot { get; private set; }

        private Gateway gateway;

        public NexaSwitch(SwitchConfiguration config, Gateway g, Channel channel)
            : base(typeof(NexaSwitch).FullName + "." + config.Name)
        {
            this.Name = config.Name;
            this.Group = config.Group;
            this.Device = config.Device;

            this.gateway = g;

            this.Slot = new Slot<bool>(this.GetType().FullName + "." + config.Name);
            this.Slot.State.AttributeChanged += this.signalChanged;

            channel.publish(this.Slot);
        }

        private void signalChanged(IAttribute a)
        {
            this.gateway.nexaDeviceOnOff(this.Group, this.Device, this.Slot.State);
        }
    }
}
