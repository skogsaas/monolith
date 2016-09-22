using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Bindings
{
    public class BindingConfiguration : Configuration.ConfigurationBase
    {
        public Framework.AttributeBase<string> BindingType;
        public Framework.AttributeBase<string> Signal;
        public Framework.AttributeBase<string> Slot;

        public BindingConfiguration() 
            : this(typeof(BindingConfiguration).Name)
        {
        }

        public BindingConfiguration(string identifier)
            : base(identifier)
        {
            this.BindingType = new Framework.AttributeBase<string>(this, "BindingType");
            this.Signal = new Framework.AttributeBase<string>(this, "Signal");
            this.Slot = new Framework.AttributeBase<string>(this, "Slot");
        }
    }
}
