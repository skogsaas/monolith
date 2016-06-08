using Monolith.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Signals
{
    class Binding<U, V>
        where U: IConvertible
        where V: IConvertible
    {
        public enum BindingMode
        {
            OneWay,
            TwoWay
        }

        public Signal<U> First { get; private set; }
        public Signal<V> Second { get; private set; }
        public BindingMode Mode { get; private set; }

        public Binding(Signal<U> u, Signal<V> v, BindingMode mode)
        {
            this.First = u;
            this.Second = v;
            this.Mode = mode;

            this.First.InnerState.AttributeChanged += (IAttribute a) => { this.Second.State = Converter<U, V>(this.First.State); };

            if(this.Mode == BindingMode.TwoWay)
            {
                this.Second.InnerState.AttributeChanged += (IAttribute a) => { this.First.State = Converter<V, U>(this.Second.State); };
            }
        }

        private static B Converter<A, B>(A value) where A : IConvertible
        {
            return (B)Convert.ChangeType(value, typeof(B));
        }
    }
}
