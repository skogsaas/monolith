using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RFBridge
{
    class Configuration
    {
        public struct NexaConfig
        {
            public string Name { get; set; }
            public uint Group { get; set; }
            public byte Device { get; set; }
            public bool Dimmable { get; set; }
        }

        public List<NexaConfig> Nexa { get; set; }

        public Configuration()
        {
            this.Nexa = new List<NexaConfig>();
        }
    }
}
