using Skogsaas.Legion;

namespace Skogsaas.Monolith.Devices
{
    public enum States : int
    {
        Uninitialized = 0,
        Initialized,
        Offline,
        Online
    }

    public interface DeviceState : IObject
    {
        States State { get; set; }
    }
}
