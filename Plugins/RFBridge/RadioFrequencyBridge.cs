using Monolith.Plugins;
using Monolith.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Monolith.Framework;

namespace RFBridge
{
    public class RadioFrequencyBridge : PluginBase
    {
        private RadioFrequencyBridgeConfiguration config;

        public Channel SignalChannel { get; private set; }

        public Gateway Transmitter { get; private set; }

        public RadioFrequencyBridge()
            : base(typeof(RadioFrequencyBridge).Name)
        {
            
        }

        public override void initialize()
        {
            base.initialize();

            this.SignalChannel = Manager.Instance.create("Signals");
            this.Transmitter = new Gateway();

            this.config = Monolith.Configuration.Manager.Instance.load<RadioFrequencyBridgeConfiguration>(this.GetType().Name + ".cfg");

            if (this.config != null)
            {
                foreach (RadioFrequencyBridgeConfiguration.NexaConfig device in this.config.Nexa)
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
}
