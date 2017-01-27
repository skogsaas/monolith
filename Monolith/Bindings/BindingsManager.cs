using Skogsaas.Legion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Skogsaas.Monolith.Bindings
{
    public class BindingsManager
    {
        private Channel bindingChannel;
        private Channel configChannel;

        private List<Binding> bindings;

        public BindingsManager()
        {
            this.bindings = new List<Binding>();

            this.bindingChannel = Manager.Create(Constants.Channel);
            this.configChannel = Manager.Create(Configuration.Constants.Channel);

            this.configChannel.SubscribePublish(typeof(IBinding), onIBindingPublished);
            this.configChannel.SubscribeUnpublish(typeof(IBinding), onIBindingUnpublished);
            this.configChannel.RegisterType(typeof(IBinding));
        }

        private void onIBindingPublished(Channel channel, IObject obj)
        {
            IBinding binding = obj as IBinding;

            add(binding);
        }

        private void onIBindingUnpublished(Channel channel, IObject obj)
        {
            IBinding binding = obj as IBinding;

            remove(binding);
        }

        private void add(IBinding binding)
        {
            this.bindings.Add(new Binding(binding, this.bindingChannel));
        }

        private void remove(IBinding binding)
        {
            foreach(Binding b in this.bindings)
            {
                if(b.Config == binding)
                {
                    this.bindings.Remove(b);
                    break;
                }
            }
        }
    }
}
