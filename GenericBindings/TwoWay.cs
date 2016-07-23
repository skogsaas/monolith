using Monolith.Bindings;
using Monolith.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monolith.Signals;

namespace Monolith.Plugins.GenericBindings
{
    class TwoWay : BindingBase
    {
        public TwoWay()
        {
        }

        public override void initialize(string identifier, Signal<IConvertible> f, Signal<IConvertible> s)
        {
            base.initialize(identifier, f, s);

            this.First.InnerState.AttributeChanged += (IAttribute a) => { this.Second.State = this.First.State; };
            this.Second.InnerState.AttributeChanged += (IAttribute a) => { this.First.State = this.Second.State; };
        }
    }
}
