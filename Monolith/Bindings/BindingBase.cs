using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Bindings
{
    public class BindingBase : IBinding
    {
        protected Framework.ObjectReference<Signals.Signal<IConvertible>> First { get; private set; }
        protected Framework.ObjectReference<Signals.Signal<IConvertible>> Second { get; private set; }
        
        protected BindingState State { get; private set; }

        public BindingBase()
        {

        }

        public virtual void initialize(BindingConfiguration config)
        {
            Framework.Channel bindingChannel = Framework.Manager.Instance.create(Bindings.Constants.Channel);

            this.State = new BindingState(config.Identifier);
            bindingChannel.publish(this.State);

            Framework.Channel signalChannel = Framework.Manager.Instance.create(Signals.Constants.Channel);

            this.First = new Framework.ObjectReference<Signals.Signal<IConvertible>>(signalChannel, config.First.Value);
            this.Second = new Framework.ObjectReference<Signals.Signal<IConvertible>>(signalChannel, config.Second.Value);

            if(this.First.Get() != null && this.Second != null)
            {
                onInitialized();
            }
            else
            {
                if(this.First.Get() == null)
                {
                    this.First.ReferenceChanged += onReferenceChanged;
                }

                if (this.Second.Get() == null)
                {
                    this.Second.ReferenceChanged += onReferenceChanged;
                }
            }
        }

        protected virtual void onInitialized()
        {
            throw new NotImplementedException();
        }

        private void onReferenceChanged(Framework.ObjectReference<Signals.Signal<IConvertible>> r)
        {
            if (this.First.Get() != null && this.Second != null)
            {
                onInitialized();
            }
        }
    }
}
