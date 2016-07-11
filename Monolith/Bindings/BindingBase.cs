using Monolith.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Bindings
{
    public class BindingBase
    {
        public ISignal From { get; protected set; }
        public ISignal To { get; protected set; }

        public BindingBase()
        {
        }

        public virtual void initialize()
        {

        }
    }
}
