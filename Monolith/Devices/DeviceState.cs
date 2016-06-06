using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Devices
{
    public class DeviceState : Framework.ObjectBase
    {
        public enum States : int
        {
            Uninitialized = 0,
            Initialized,
            Offline,
            Online
        }

        private Framework.AttributeBase<States> state;

        public States State
        {
            get { return this.state.Value; }
            set { this.state.Value = value; }
        }

		public DeviceState(string identifier)
            : base(identifier)
        {
            this.state = new Framework.AttributeBase<States>(this);
        }
    }
}
