using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Signals
{
    class StringSignal : SignalBase<string>
    {
        public StringSignal(string identifier)
            : base(identifier)
        {

        }
    }
}
