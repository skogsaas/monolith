using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Configuration
{
    public class ConfigurationManagerProxy : Framework.ObjectBase
    {
        public Framework.Action<bool, Type> Register;
        public Framework.AttributeCollectionBase<Framework.AttributeBase<string>> Types;

        public ConfigurationManagerProxy()
            : base(Constants.ConfigurationManagerProxyIdentifier)
        {
            this.Register = new Framework.Action<bool, Type>(this, "Register");
            this.Types = new Framework.AttributeCollectionBase<Framework.AttributeBase<string>>(this, "Types");
        }
    }
}
