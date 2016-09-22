using Monolith.Bindings;
using Monolith.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monolith.Signaling;

namespace Monolith.Plugins.GenericBindings
{
    public class OneWay : BindingBase
    {
        public OneWay()
        {
        }

        protected override void onInitialized()
        {
            this.Signal.Get().State.AttributeChanged += State_AttributeChanged;
        }

        private void State_AttributeChanged(IAttribute a)
        {
            this.Slot.Get().State.Value = this.Signal.Get().State.Value;
        }
    }
}
