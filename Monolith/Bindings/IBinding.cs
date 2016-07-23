using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Bindings
{
    public interface IBinding
    {
        void initialize(string identifier, Signals.Signal<IConvertible> f, Signals.Signal<IConvertible> s);
    }
}
