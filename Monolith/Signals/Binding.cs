using Monolith.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Signals
{
    class Binding<U, V>
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

            this.First.InnerState.AttributeChanged += (IAttribute a) => { sync(this.First, this.Second); };

            if(this.Mode == BindingMode.TwoWay)
            {
                this.Second.InnerState.AttributeChanged += (IAttribute a) => { sync(this.Second, this.First); };
            }
        }

        private void sync<A, B>(Signal<A> from, Signal<B> to)
        {
            to.State = from.State;
        }
    }
}
