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
    class DevicesApi : IApi
    {
        private Channel channel;

        private Dictionary<string, Devices.DeviceState> devices;

        public static string Path
        {
            get
            {
                return "devices";
            }
        }

        public DevicesApi()
        {
            this.channel = Framework.Manager.Instance.create(Plugins.Constants.Channel);
            this.devices = new Dictionary<string, Devices.DeviceState>();
            this.channel.subscribe(typeof(Devices.DeviceState), this.onObject);
        }

        public void handle(HttpListenerContext context)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(this.devices, Formatting.Indented);
            byte[] data = Encoding.UTF8.GetBytes(json);

            context.Response.ContentLength64 = data.Length;
            context.Response.OutputStream.Write(data, 0, data.Length);
            context.Response.Close();
        }

        private void onObject(Channel c, IObject obj)
        {
            if (typeof(Devices.DeviceState).IsAssignableFrom(obj.GetType()))
            {
                this.devices[obj.Identifier] = (Devices.DeviceState)obj;
            }
        }
    }
}
