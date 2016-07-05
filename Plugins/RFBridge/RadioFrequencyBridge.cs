using Monolith.Plugins;
using Monolith.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RFBridge
{
    public class RadioFrequencyBridge : PluginBase
    {
        private Configuration config;

        public Monolith.Framework.Channel SignalChannel { get; private set; }

        public Gateway Transmitter { get; private set; }

        public RadioFrequencyBridge()
            : base(typeof(RadioFrequencyBridge).Name)
        {
            this.config = new Configuration();
        }

        public override void initialize()
        {
            base.initialize();

            this.SignalChannel = Monolith.Framework.Manager.Instance.create(Monolith.Signals.Constants.Channel);
            this.Transmitter = new Gateway();

            configure(this.config);
            this.config.ConfigurationChanged += configure;
        }

        private void configure(Monolith.Configuration.ConfigurationBase configuration)
        {
            foreach (Configuration.NexaConfig device in this.config.Nexa)
            {
                if (device.Dimmable)
                {
                    NexaDimmer d = new NexaDimmer(this, device);
                }
                else
                {
                    NexaSwitch s = new NexaSwitch(this, device);
                }
            }
        }
    }
}
