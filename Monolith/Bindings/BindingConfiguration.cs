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
        public Framework.AttributeBase<string> First;
        public Framework.AttributeBase<string> Second;

        public BindingConfiguration() 
            : base(typeof(BindingConfiguration).Name)
        {
            this.BindingType = new Framework.AttributeBase<string>(this, "BindingType");
            this.First = new Framework.AttributeBase<string>(this, "First");
            this.Second = new Framework.AttributeBase<string>(this, "Second");
        }
    }
}
