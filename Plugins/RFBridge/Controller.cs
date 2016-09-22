using Monolith.Plugins;
using Monolith.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Monolith.Framework;

namespace NexaController
{
    public class Controller : PluginBase
    {
        private Monolith.Framework.Channel configChannel;
        private Monolith.Framework.Channel signalChannel;

        private List<NexaSwitch> switches;
        private List<NexaDimmer> dimmers;

        private Gateway gateway;

        public Controller()
            : base("NexaController")
        {
            this.switches = new List<NexaSwitch>();
            this.dimmers = new List<NexaDimmer>();
        }

        public override void initialize()
        {
            base.initialize();

            this.configChannel = Manager.Instance.create(Monolith.Configuration.Constants.Channel);
            this.signalChannel = Manager.Instance.create(Monolith.Signaling.Constants.Channel);

            this.configChannel.subscribePublish(typeof(SwitchConfiguration), onSwitchConfiguration);
            this.configChannel.subscribePublish(typeof(DimmerConfiguration), onDimmerConfiguration);

            this.gateway = new Gateway();
        }

        private void onSwitchConfiguration(Channel channel, IObject obj)
        {
            SwitchConfiguration c = obj as SwitchConfiguration;

            this.switches.Add(new NexaSwitch(c, this.gateway, this.signalChannel));
        }

        private void onDimmerConfiguration(Channel channel, IObject obj)
        {
            DimmerConfiguration c = obj as DimmerConfiguration;

            this.dimmers.Add(new NexaDimmer(c, this.gateway, this.signalChannel));
        }
    }
}
