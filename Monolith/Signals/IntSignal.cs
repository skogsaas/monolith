using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Signals
{
    class IntSignal : SignalBase<int>
    {
        public IntSignal(string identifier)
            : base(identifier)
        {

        }
    }
}
