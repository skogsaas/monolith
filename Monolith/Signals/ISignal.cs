using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Signals
{
    public interface ISignal
    {
        string Identifier { get; }
        Type SignalType { get; }
    }
}
