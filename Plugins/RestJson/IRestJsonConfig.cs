using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skogsaas.Monolith.Plugins.RestJson
{
    public interface IRestJsonConfig : Configuration.Identifier
    {
        int Port { get; set; }
        List<string> Channels { get; set; }
    }
}
