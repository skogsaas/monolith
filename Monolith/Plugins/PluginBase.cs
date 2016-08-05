using Monolith.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Plugins
{
    public class PluginBase : IPlugin
    {
        private Channel pluginChannel;
        private PluginState pluginState;

        public PluginBase(string pluginName)
        {
            this.pluginChannel = Manager.Instance.create(Constants.Channel);

            this.pluginState = new PluginState(pluginName);
            this.pluginChannel.publish(this.pluginState);
        }

        private PluginBase()
        {

        }

        public virtual void initialize()
        {
            this.pluginState.State.Value = PluginState.States.Initialized;
        }
    }
}
