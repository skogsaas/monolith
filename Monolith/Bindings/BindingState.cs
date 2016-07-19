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

        private Framework.AttributeBase<States> state;

        public States State
        {
            get { return this.state.Value; }
            set { this.state.Value = value; }
        }

        public BindingState(string identifier)
            : base(identifier)
        {
            this.state = new Framework.AttributeBase<States>(this, "State");
        }
    }
}
