using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Bindings
{
    public class BindingBase : IBinding
    {
        protected Framework.ObjectReference<Signaling.Signal<IConvertible>> Signal { get; private set; }
        protected Framework.ObjectReference<Signaling.Slot<IConvertible>> Slot { get; private set; }
        
        protected BindingState State { get; private set; }

        public BindingBase()
        {

        }

        public virtual void initialize(BindingConfiguration config)
        {
            Framework.Channel bindingChannel = Framework.Manager.Instance.create(Bindings.Constants.Channel);

            this.State = new BindingState(config.Identifier);
            bindingChannel.publish(this.State);

            Framework.Channel signalChannel = Framework.Manager.Instance.create(Signaling.Constants.Channel);

            this.Signal = new Framework.ObjectReference<Signaling.Signal<IConvertible>>(signalChannel, config.Signal.Value);
            this.Slot = new Framework.ObjectReference<Signaling.Slot<IConvertible>>(signalChannel, config.Slot.Value);

            if(this.Signal.Get() != null && this.Slot != null)
            {
                onInitialized();
            }
            else
            {
                if(this.Signal.Get() == null)
                {
                    this.Signal.ReferenceChanged += onReferenceChanged;
                }

                if (this.Slot.Get() == null)
                {
                    this.Slot.ReferenceChanged += onReferenceChanged;
                }
            }
        }

        protected virtual void onInitialized()
        {
            throw new NotImplementedException();
        }

        private void onReferenceChanged(Framework.ObjectReference<Signaling.Signal<IConvertible>> r)
        {
            if (this.Signal.Get() != null && this.Slot != null)
            {
                onInitialized();
            }
        }

        private void onReferenceChanged(Framework.ObjectReference<Signaling.Slot<IConvertible>> r)
        {
            if (this.Signal.Get() != null && this.Slot != null)
            {
                onInitialized();
            }
        }
    }
}
