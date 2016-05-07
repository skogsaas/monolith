using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith
{
    public delegate void SignalEventHandler(ISignal signal);

    public interface ISignal
    {
        event SignalEventHandler SignalChanging;
        event SignalEventHandler SignalChanged;
    }
}
