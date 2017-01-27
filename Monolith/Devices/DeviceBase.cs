using Skogsaas.Legion;

namespace Skogsaas.Monolith.Devices
{
    public class DeviceBase : IDevice
    {
        private Channel deviceChannel;
        private DeviceState deviceState;

        public DeviceBase(string deviceName)
        {
            this.deviceChannel = Manager.Create(Constants.Channel);

            this.deviceState = this.deviceChannel.CreateType<DeviceState>(deviceName);
            this.deviceChannel.Publish(this.deviceState);
        }

        private DeviceBase()
        {

        }

        public virtual void initialize()
        {
            this.deviceState.State = States.Initialized;
        }
    }
}
