using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Bindings
{
    public class BindingManager
    {
        private Framework.Channel systemChannel;
        private Framework.Channel configChannel;

        private Dictionary<string, BindingConfiguration> configurations;
        private Dictionary<string, BindingBase> bindings;
        private Dictionary<string, Type> types;

        private BindingManagerProxy proxy;

        private Framework.ObjectReference<Configuration.ConfigurationManagerProxy> configManager;
        private Framework.ObjectReference<BindingConfiguration> configuration;

        public BindingManager()
        {
            this.systemChannel = Framework.Manager.Instance.create(Core.Constants.Channel);
            this.configChannel = Framework.Manager.Instance.create(Configuration.Constants.Channel);

            this.configurations = new Dictionary<string, BindingConfiguration>();
            this.bindings = new Dictionary<string, BindingBase>();
            this.types = new Dictionary<string, Type>();

            this.proxy = new BindingManagerProxy();
            this.proxy.Register.SetHandler(onRegisterType);
            this.systemChannel.publish(this.proxy);

            this.configManager = new Framework.ObjectReference<Configuration.ConfigurationManagerProxy>(this.systemChannel, Configuration.Constants.ConfigurationManagerProxyIdentifier);

            if (this.configManager.Get() != null)
            {
                onConfigManagerReferenced(this.configManager);
            }
            else
            {
                this.configManager.ReferenceChanged += onConfigManagerReferenced;
            }

            this.configuration = new Framework.ObjectReference<BindingConfiguration>(configChannel, Constants.ConfigurationIdentifier);

            if (this.configuration.Get() != null)
            {
                onConfigurationReferenced(this.configuration);
            }
            else
            {
                this.configuration.ReferenceChanged += onConfigurationReferenced;
            }
        }

        private void onConfigManagerReferenced(Framework.ObjectReference<Configuration.ConfigurationManagerProxy> r)
        {
            bool result = this.configManager.Get().Register.Execute(typeof(BindingConfiguration));
        }

        private void onConfigurationReferenced(Framework.ObjectReference<BindingConfiguration> r)
        {
            BindingConfiguration config = this.configuration.Get();
            BindingBase binding = createBinding(config.BindingType);

            binding.initialize(config);

            this.configurations.Add(config.Identifier, config);
            this.bindings.Add(config.Identifier, binding);
        }

        private bool onRegisterType(Type type)
        {
            if(typeof(IBinding).IsAssignableFrom(type))
            {
                this.types.Add(type.Name, type);
                this.proxy.Types.Add(type.Name, new Framework.AttributeBase<string>(this.proxy.Types, type.Name));

                return true;
            }

            return false;
        }

        private BindingBase createBinding(string type)
        {
            if(this.proxy.Types.ContainsKey(type))
            {
                return (BindingBase)Activator.CreateInstance(this.types[type]);
            }

            return null;
        }
    }
}
