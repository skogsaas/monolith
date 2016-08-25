using Monolith.Plugins;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Plugins.StaticConfig
{
    public class Loader : IPlugin
    {
        Framework.Channel configChannel;
        Bindings.BindingConfiguration bindingsConfig;

        public Loader()
        {

        }

        public void initialize()
        {
            this.configChannel = Framework.Manager.Instance.create(Configuration.Constants.Channel);

            this.bindingsConfig = new Bindings.BindingConfiguration();
            this.configChannel.publish(this.bindingsConfig);
        }
    }
}
