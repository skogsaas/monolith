using Monolith.Bindings;
using Monolith.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monolith.Signals;

namespace Monolith.Plugins.GenericBindings
{
    class TwoWay<U, V> : BindingBase<U, V>
        where U : IConvertible
        where V : IConvertible
    {
        public TwoWay()
        {
        }

        public override void initialize(string identifier, Signal<U> f, Signal<V> s)
        {
            base.initialize(identifier, f, s);

            this.First.InnerState.AttributeChanged += (IAttribute a) => { this.Second.State = Converter<U, V>(this.First.State); };
            this.Second.InnerState.AttributeChanged += (IAttribute a) => { this.First.State = Converter<V, U>(this.Second.State); };
        }

        private static B Converter<A, B>(A value) where A : IConvertible
        {
            return (B)Convert.ChangeType(value, typeof(B));
        }
    }
}
