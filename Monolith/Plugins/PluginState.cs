using Skogsaas.Legion;

namespace Skogsaas.Monolith.Plugins
{
    public enum PluginStates
    {
        Uninitialized = 0,
        Initialized,
        Offline,
        Online
    }

    public interface PluginState : IObject
    {
        PluginStates State { get; set; }
    }
}
