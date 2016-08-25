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
    public class OneWay : BindingBase
    {
        public OneWay()
        {
        }

        protected override void onInitialized()
        {
            this.First.Get().State.AttributeChanged += State_AttributeChanged;
        }

        private void State_AttributeChanged(IAttribute a)
        {
            this.Second.Get().State.Value = this.First.Get().State.Value;
        }
    }
}
