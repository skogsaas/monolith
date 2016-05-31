using Monolith.Plugins;
using Monolith.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Monolith.Framework;

namespace RFBridge
{
    public class RadioFrequencyBridge : PluginBase
    {
        private Gateway gateway;
        private Configuration config;

        public Channel SignalChannel { get; private set; }

        public RadioFrequencyBridge()
            : base("RadioFrequenceyBridge")
        {
            
        }

        public override void initialize()
        {
            base.initialize();

            this.SignalChannel = Manager.Instance.create("Signals");
            this.gateway = new Gateway();

            loadConfiguration();

            //this.task = Monolith.Utilities.PeriodicTask.StartPeriodicTask(this.trigger, 10000, new CancellationToken());
        }

        private void loadConfiguration()
        {
            try
            {
                string data = File.ReadAllText("RFBridge.json");
                this.config = JsonConvert.DeserializeObject<Configuration>(data);

                foreach(Configuration.NexaConfig device in this.config.Nexa)
                {
                    if(device.Dimmable)
                    {
                        NexaDimmer d = new NexaDimmer(this, device);
                    }
                    else
                    {
                        NexaSwitch s = new NexaSwitch(this, device);
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}
