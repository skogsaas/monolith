using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Configuration
{
    public interface IConfiguration
    {
        string Identifier { get; }
    }
}
