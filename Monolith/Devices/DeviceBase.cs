using Monolith.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Devices
{
    public class DeviceBase : IDevice
    {
        private Channel deviceChannel;
        private DeviceState deviceState;

        public DeviceBase(string deviceName)
        {
            this.deviceChannel = Manager.Instance.create("Devices");

            this.deviceState = new DeviceState(deviceName);
            this.deviceChannel.publish(this.deviceState);
        }

        private DeviceBase()
        {

        }

        public virtual void initialize()
        {
            this.deviceState.State = DeviceState.States.Initialized;
        }
    }
}
