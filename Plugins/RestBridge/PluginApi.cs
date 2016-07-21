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
    class PluginApi : IApi
    {
        private Channel channel;

        public static string Path
        {
            get
            {
                return "plugin";
            }
        }

        public PluginApi()
        {
            this.channel = Framework.Manager.Instance.create(Plugins.Constants.Channel);
        }

        public void handle(HttpListenerContext context)
        {
            string identifier = context.Request.RawUrl.Substring(("/rest/plugin/").Length);

            PluginState plugin = (PluginState)this.channel.find(identifier);

            if (plugin != null)
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(plugin, Formatting.Indented);
                byte[] data = Encoding.UTF8.GetBytes(json);

                context.Response.ContentLength64 = data.Length;
                context.Response.OutputStream.Write(data, 0, data.Length);
            }

            context.Response.Close();
        }
    }
}
