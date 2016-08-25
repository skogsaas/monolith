using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Bindings
{
    public class BindingManagerProxy : Framework.ObjectBase
    {
        public Framework.Action<bool, Type> Register;
        public Framework.AttributeCollectionBase<Framework.AttributeBase<string>> Types;

        public BindingManagerProxy()
            : base(Constants.BindingManagerProxyIdentifier)
        {
            this.Register = new Framework.Action<bool, Type>(this, "Register");
            this.Types = new Framework.AttributeCollectionBase<Framework.AttributeBase<string>>(this, "Types");
        }
    }
}
