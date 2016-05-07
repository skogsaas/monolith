using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Signals
{
    class DoubleSignal : SignalBase<double>
    {
        public DoubleSignal(string identifier)
            : base(identifier)
        {

        }
    }
}
