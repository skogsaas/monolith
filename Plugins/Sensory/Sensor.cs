using Monolith.Signaling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Plugins.Sensory
{
    public class Sensor
    {
        public byte Id { get; protected set; }
        public string Type { get { return this.GetType().Name; } }
        public List<ISignaling> Signals { get; protected set; }

        protected Sensor()
        {
            this.Signals = new List<ISignaling>();
        }

        public virtual void handle(DeviceMessage msg)
        {
            throw new NotImplementedException();
        }
    }
}
