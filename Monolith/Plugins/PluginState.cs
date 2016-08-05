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

        public Framework.AttributeBase<States> State { get; private set; }

        public PluginState(string identifier)
            : base(identifier)
        {
            this.State = new Framework.AttributeBase<States>(this, "State");
        }
    }
}
