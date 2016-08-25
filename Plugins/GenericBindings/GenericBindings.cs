using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monolith.Bindings;

namespace Monolith.Plugins.GenericBindings
{
    public class GenericBindings : PluginBase
    {
        private Framework.Channel bindingChannel;
        private Framework.ObjectReference<Bindings.BindingManagerProxy> bindingManager;

        public GenericBindings()
            : base("GenericBindings")
        {
            
        }

        public override void initialize()
        {
            base.initialize();

            this.bindingChannel = Framework.Manager.Instance.create(Bindings.Constants.Channel);
            this.bindingManager = new Framework.ObjectReference<BindingManagerProxy>(this.bindingChannel, Bindings.Constants.BindingManagerProxyIdentifier);

            this.bindingManager.ReferenceChanged += onReferenceChanged;
        }

        private void onReferenceChanged(Framework.ObjectReference<Bindings.BindingManagerProxy> r)
        {
            bool success = r.Get().Register.Execute(typeof(OneWay));
        }
    }
}
