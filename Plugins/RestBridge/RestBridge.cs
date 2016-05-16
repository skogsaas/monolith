using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monolith.Plugins;
using Monolith.Framework;
using System.IO;
using Newtonsoft.Json;
using System.Net;

namespace Monolith.Plugins.REST
{
    public class RestBridge : PluginBase
    {
        private Channel pluginChannel;
		private Channel deviceChannel;
        private List<PluginState> plugins;
		private List<DeviceState> devices;

        private HttpListener listener;

        public RestBridge()
            : base("RestBridge")
        {
            this.plugins = new List<PluginState>();
			this.devices = new List<DeviceState>();
        }

        public override void initialize()
        {
            base.initialize();

			this.pluginChannel = Manager.Instance.create("Plugins");
			this.pluginChannel.subscribe(typeof(PluginState), onObject);

			this.deviceChannel = Manager.Instance.create("Devices");
			this.deviceChannel.subscribe(typeof(DeviceState), onObject);

            this.listener = new HttpListener();
            this.listener.Prefixes.Add("http://+:8080/rest/");
            this.listener.Start();

            Task.Factory.StartNew(() => listen());
        }

        private async void listen()
        {
            while(true)
            {
                HttpListenerContext context = await this.listener.GetContextAsync();
                await Task.Factory.StartNew(() => process(context));
            }
        }

        private void onObject(Channel channel, IObject obj)
        {
            if(obj.GetType().IsAssignableFrom(typeof(PluginState)))
            {
                this.plugins.Add((PluginState)obj);
            }
			else if(obj.GetType().IsAssignableFrom(typeof(DeviceState)))
			{
				this.devices.Add((DeviceState)obj);
			}
        }

        private void process(HttpListenerContext context)
        {
            string path = context.Request.RawUrl;

            if(path.StartsWith("/rest/plugins"))
            {
                handlePlugins(context);
            }
            else if (path.StartsWith("/rest/devices"))
            {
                handleDevices(context);
            }
            else
            {
                context.Response.Close();
            }
        }

        private void handlePlugins(HttpListenerContext context)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(this.plugins, Formatting.Indented);
            byte[] data = Encoding.UTF8.GetBytes(json);

            context.Response.ContentLength64 = data.Length;
            context.Response.OutputStream.Write(data, 0, data.Length);
            context.Response.Close();
        }

        private void handleDevices(HttpListenerContext context)
        {
			string json = Newtonsoft.Json.JsonConvert.SerializeObject(this.devices, Formatting.Indented);
			byte[] data = Encoding.UTF8.GetBytes(json);

			context.Response.ContentLength64 = data.Length;
			context.Response.OutputStream.Write(data, 0, data.Length);
			context.Response.Close();
        }
    }
}