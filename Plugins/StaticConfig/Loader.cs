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
        Sensory.SensoryConfiguration sensoryConfig;

        public Loader()
        {

        }

        public void initialize()
        {
            this.configChannel = Framework.Manager.Instance.create(Configuration.Constants.Channel);

            this.bindingsConfig = new Bindings.BindingConfiguration();
            this.configChannel.publish(this.bindingsConfig);

            this.sensoryConfig = new Sensory.SensoryConfiguration("SensoryConfiguration");
            this.sensoryConfig.IpAddress.Value = "192.168.1.115";
            this.sensoryConfig.Port.Value = 5001;

            Framework.String dhtSensor = new Framework.String(this.sensoryConfig.Sensors, "1");
            dhtSensor.Value = typeof(Sensory.DhtSensor).Name;
            this.sensoryConfig.Sensors.Add("1", dhtSensor);

            this.configChannel.publish(this.sensoryConfig);
        }
    }
}
