using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Signals
{
    class BoolSignal : SignalBase<bool>
    {
        public BoolSignal(string identifier)
            : base(identifier)
        {

        }
    }
}
