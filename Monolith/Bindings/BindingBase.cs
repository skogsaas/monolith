using Monolith.Signals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Bindings
{
    public class BindingBase : IBinding
    {
        protected Signal<IConvertible> First { get; private set; }
        protected Signal<IConvertible> Second { get; private set; }

        private Framework.Channel bindingChannel;
        protected BindingState State { get; private set; }

        public BindingBase()
        {
            this.bindingChannel = Framework.Manager.Instance.create(Constants.Channel);
        }

        public virtual void initialize(string identifier, Signal<IConvertible> f, Signal<IConvertible> s)
        {
            this.First = f;
            this.Second = s;

            this.State = new BindingState(identifier);
            this.bindingChannel.publish(this.State);
        }
    }
}
