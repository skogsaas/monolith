using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Plugins
{
    public class PluginState : Framework.ObjectBase
    {
        public enum States
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

        public PluginState(string identifier)
            : base(identifier)
        {
            this.state = new Framework.AttributeBase<States>(this, "State");
        }
    }
}
