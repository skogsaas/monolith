using Skogsaas.Legion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skogsaas.Monolith.Configuration
{
    public interface IIdentifier : IObject
    {
        string Typename { get; set; }
        string Plugin { get; set; }
    }
}
