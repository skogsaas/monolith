using Monolith.Framework;
using Monolith.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexaController
{
    class SwitchConfiguration : ConfigurationBase
    {
        public Monolith.Framework.String Name { get; private set; }
        public Monolith.Framework.UInt Group { get; private set; }
        public Monolith.Framework.Byte Device { get; private set; }

        public SwitchConfiguration(string identifier)
            : base(identifier)
        {
            this.Name = new Monolith.Framework.String(this, "Name");
            this.Group = new Monolith.Framework.UInt(this, "Group");
            this.Device = new Monolith.Framework.Byte(this, "Device");
        }
    }
}
