using Monolith.Framework;
using Monolith.Signaling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monolith.Devices;

namespace NexaController
{
    class NexaDimmer : DeviceBase
    {
        public string Name { get; set; }
        public uint Group { get; set; }
        public byte Device { get; set; }

        public Slot<int> Slot { get; private set; }

        private Gateway gateway;

        public NexaDimmer(DimmerConfiguration config, Gateway g, Channel channel)
            : base(typeof(NexaDimmer).FullName + "." + config.Name)
        {
            this.Name = config.Name;
            this.Group = config.Group;
            this.Device = config.Device;

            this.gateway = g;

            this.Slot = new Slot<int>(this.GetType().FullName + "." + config.Name);
            this.Slot.State.AttributeChanged += this.signalChanged;

            channel.publish(this.Slot);
        }

        private void signalChanged(IAttribute a)
        {
            this.gateway.nexaDeviceDim(this.Group, this.Device, (byte)this.Slot.State);
        }
    }
}
