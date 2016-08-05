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
    class OneWay : BindingBase
    {
        public OneWay()
        {
        }

        public override void initialize(string identifier, Signal<IConvertible> f, Signal<IConvertible> s)
        {
            base.initialize(identifier, f, s);

            this.First.State.AttributeChanged += (IAttribute a) => { this.Second.State.Value = this.First.State.Value; };
        }
    }
}
