using Skogsaas.Legion;

namespace Skogsaas.Monolith.Plugins
{
    public class PluginBase : IPlugin
    {
        private Channel pluginChannel;
        private PluginState pluginState;

        public PluginBase(string pluginName)
        {
            this.pluginChannel = Manager.Create(Constants.Channel);

            this.pluginState = this.pluginChannel.CreateType<PluginState>(pluginName);
            this.pluginChannel.Publish(this.pluginState);
        }

        private PluginBase()
        {

        }

        public virtual void initialize()
        {
            this.pluginState.State = PluginStates.Initialized;
        }
    }
}
