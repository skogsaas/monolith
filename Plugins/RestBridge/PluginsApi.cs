using Monolith.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Plugins.REST
{
    class PluginsApi : IApi
    {
        private Channel channel;

        private Dictionary<string, Plugins.PluginState> plugins;

        public static string Path
        {
            get
            {
                return "plugins";
            }
        }

        public PluginsApi()
        {
            this.channel = Framework.Manager.Instance.create(Plugins.Constants.Channel);
            this.plugins = new Dictionary<string, PluginState>();
            this.channel.subscribe(typeof(Plugins.PluginState), this.onObject);
        }

        public void handle(HttpListenerContext context)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(this.plugins, Formatting.Indented);
            byte[] data = Encoding.UTF8.GetBytes(json);

            context.Response.ContentLength64 = data.Length;
            context.Response.OutputStream.Write(data, 0, data.Length);
            context.Response.Close();
        }

        private void onObject(Channel c, IObject obj)
        {
            if (typeof(PluginState).IsAssignableFrom(obj.GetType()))
            {
                this.plugins[obj.Identifier] = (PluginState)obj;
            }
        }
    }
}
