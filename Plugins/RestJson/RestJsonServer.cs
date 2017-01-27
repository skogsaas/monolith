using System;
using Skogsaas.Monolith.Plugins;
using Skogsaas.Legion;
using System.Collections.Generic;

namespace Skogsaas.Monolith.Plugins.RestJson
{
    public class RestJsonServer : IPlugin
    {
        private Skogsaas.Legion.RestJson.Server server;

        private Channel configChannel; 

        public void initialize()
        {
            this.configChannel = Manager.Create(Configuration.Constants.Channel);
            this.configChannel.SubscribePublish(typeof(IRestJsonConfig), onConfigPublished);
        }

        private void onConfigPublished(Channel c, IObject obj)
        {
            if(this.server == null)
            {
                IRestJsonConfig config = (IRestJsonConfig)obj;

                List<Channel> channels = new List<Channel>();

                foreach(string name in config.Channels)
                {
                    channels.Add(Manager.Create(name));
                }

                this.server = new Skogsaas.Legion.RestJson.Server(config.Port, channels.ToArray());
                this.server.Start();
            }
        }
    }
}
