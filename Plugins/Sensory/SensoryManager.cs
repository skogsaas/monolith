using System;
using System.Net;
using System.Collections.Generic;

namespace Monolith.Plugins.Sensory
{
    public class SensoryManager : IPlugin
    {
        private Framework.Channel coreChannel;
        private Framework.Channel configChannel;
        private Framework.Channel signalChannel;

        private Framework.ObjectReference<Configuration.ConfigurationManagerProxy> configManager;

        private Dictionary<string, Device> devices;

        private Gateway gateway;

        public SensoryManager()
        {
            this.devices = new Dictionary<string, Device>();

            this.gateway = new Gateway();
            this.gateway.Packets += onMessage;
        }

        public void initialize()
        {
            this.coreChannel = Framework.Manager.Instance.create(Core.Constants.Channel);
            this.configChannel = Framework.Manager.Instance.create(Configuration.Constants.Channel);
            this.signalChannel = Framework.Manager.Instance.create(Signaling.Constants.Channel);

            this.configManager = new Framework.ObjectReference<Configuration.ConfigurationManagerProxy>(this.coreChannel, Configuration.Constants.ConfigurationManagerProxyIdentifier);
            if (this.configManager.Get() != null)
            {
                onConfigManagerReferenced(this.configManager);
            }
            else
            {
                this.configManager.ReferenceChanged += onConfigManagerReferenced;
            }

            this.configChannel.subscribePublish(typeof(SensoryConfiguration), onConfigurationPublish);
        }

        private void onConfigManagerReferenced(Framework.ObjectReference<Configuration.ConfigurationManagerProxy> r)
        {
            r.Get().Register.Execute(typeof(SensoryConfiguration));
        }

        private void onConfigurationPublish(Framework.Channel c, Framework.IObject obj)
        {
            SensoryConfiguration config = (SensoryConfiguration)obj;

            Device device = new Device(this.gateway, this.signalChannel, config);
            this.devices.Add(config.Identifier, device);
        }

        private void onMessage(byte[] data, IPAddress addr, ushort port)
        {
            foreach(KeyValuePair<string, Device> pair in this.devices)
            {
                if(pair.Value.Address.Equals(addr))
                {
                    pair.Value.Handle(data);

                    break;
                }
            }
        }
    }
}
