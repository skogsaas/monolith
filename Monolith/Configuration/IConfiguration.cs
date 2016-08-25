using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Configuration
{
    public interface IConfiguration
    {
        string Type { get; }
        string Plugin { get; }
        string Identifier { get; }
    }
}
