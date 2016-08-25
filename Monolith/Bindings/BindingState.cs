using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Bindings
{
    public class BindingState : Framework.ObjectBase
    {
        public enum States
        {
            Uninitialized = 0,
            Initialized
        }

        public Framework.AttributeBase<States> State;

        public BindingState(string identifier)
            : base(identifier)
        {
            this.State = new Framework.AttributeBase<States>(this, "State");
        }
    }
}
