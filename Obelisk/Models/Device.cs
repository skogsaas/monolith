using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obelisk.Models
{
    public class Device
    {
        public string Identifier { get; set; }
        public string Type { get; set; }
        public Plugin Plugin { get; set; }

        public Device()
        {
            this.Identifier = "Device Name";
            this.Type = "Device Type";
            this.Plugin = null;
        }
    }
}
