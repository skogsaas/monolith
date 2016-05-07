using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Signals
{
    class LongSignal : Signal<long>
    {
        public LongSignal(string identifier)
            : base(identifier)
        {

        }
    }
}
