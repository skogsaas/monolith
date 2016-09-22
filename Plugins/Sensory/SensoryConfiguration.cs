using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Plugins.Sensory
{
    public class SensoryConfiguration : Configuration.ConfigurationBase
    {
        public Framework.String IpAddress { get; set; }
        public Framework.UShort Port { get; set; }

        public Framework.AttributeCollectionBase<Framework.String> Sensors { get; set; }

        public SensoryConfiguration(string identifier)
            : base(identifier)
        {
            this.IpAddress = new Framework.String(this, "IpAddress");
            this.Port = new Framework.UShort(this, "Port");

            this.Sensors = new Framework.AttributeCollectionBase<Framework.String>(this, "Sensors");
        }
    }
}
