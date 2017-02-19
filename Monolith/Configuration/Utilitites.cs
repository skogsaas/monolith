using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skogsaas.Monolith.Configuration
{
    public class Utilities
    {
        public static void PrepareConfiguration(IIdentifier identifier)
        {
            identifier.Typename = identifier.GetInterface().FullName;
            identifier.Plugin = identifier.GetInterface().Namespace;
        }
    }
}
