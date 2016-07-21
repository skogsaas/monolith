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
    class DeviceApi : IApi
    {
        private Channel channel;

        public static string Path
        {
            get
            {
                return "device";
            }
        }

        public DeviceApi()
        {
            this.channel = Framework.Manager.Instance.create(Devices.Constants.Channel);
        }

        public void handle(HttpListenerContext context)
        {
            string identifier = context.Request.RawUrl.Substring(("/rest/device/").Length);

            Devices.DeviceState device = (Devices.DeviceState)this.channel.find(identifier);

            if (device != null)
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(device, Formatting.Indented);
                byte[] data = Encoding.UTF8.GetBytes(json);

                context.Response.ContentLength64 = data.Length;
                context.Response.OutputStream.Write(data, 0, data.Length);
            }

            context.Response.Close();
        }
    }
}
