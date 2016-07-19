using Monolith.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Bindings
{
    public class BindingBase<U, V> : IBinding
        where U : IConvertible
        where V : IConvertible
    {
        protected Signal<U> First { get; private set; }
        protected Signal<V> Second { get; private set; }

        private Framework.Channel bindingChannel;
        protected BindingState State { get; private set; }

        public BindingBase()
        {
            this.bindingChannel = Framework.Manager.Instance.create(Constants.Channel);
        }

        public virtual void initialize(string identifier, Signal<U> f, Signal<V> s)
        {
            this.First = f;
            this.Second = s;

            this.State = new BindingState(identifier);
            this.bindingChannel.publish(this.State);
        }
    }
}
