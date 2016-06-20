using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monolith.Plugins;
using Monolith.Framework;
using Monolith.Signals;
using Newtonsoft.Json;
using System.Net;
using Monolith.Devices;
using System.Net.WebSockets;
using System.IO;

namespace Monolith.Plugins.REST
{
    public class RestBridge : PluginBase
    {
        private Channel pluginChannel;
		private Channel deviceChannel;
        private Channel signalChannel;

        private DataModel model;

        private HttpListener listener;

        List<WebSocketHandler> webSockets;

        public RestBridge()
            : base("RestBridge")
        {
            this.model = new DataModel();

            this.webSockets = new List<WebSocketHandler>();
        }

        public override void initialize()
        {
            base.initialize();

			this.pluginChannel = Manager.Instance.create("Plugins");
			this.pluginChannel.subscribe(typeof(PluginState), onObject);

			this.deviceChannel = Manager.Instance.create("Devices");
			this.deviceChannel.subscribe(typeof(DeviceState), onObject);

            this.signalChannel = Manager.Instance.create("Signals");
            this.signalChannel.subscribe(typeof(ISignal), onObject);

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
            if(typeof(PluginState).IsAssignableFrom(obj.GetType()))
            {
                this.model.Plugins.Add((PluginState)obj);
            }
			else if(typeof(DeviceState).IsAssignableFrom(obj.GetType()))
			{
				this.model.Devices.Add((DeviceState)obj);
			}
            else if(typeof(ISignal).IsAssignableFrom(obj.GetType()))
            {
                this.model.Signals.Add((ISignal)obj);
            }
        }

        private async void process(HttpListenerContext context)
        {
            if(context.Request.IsWebSocketRequest)
            {
                HttpListenerWebSocketContext c = await context.AcceptWebSocketAsync(null);

                WebSocketHandler h = new WebSocketHandler(c.WebSocket, this.model);
                h.Disconnected += websocketDisconnected;
                this.webSockets.Add(h);
            }
            else
            {
                string path = context.Request.RawUrl;

                if (path.StartsWith("/rest/plugins"))
                {
                    handlePlugins(context);
                }
                if (path.StartsWith("/rest/plugin"))
                {
                    handlePlugin(context);
                }
                else if (path.StartsWith("/rest/devices"))
                {
                    handleDevices(context);
                }
                else if (path.StartsWith("/rest/device"))
                {
                    handleDevice(context);
                }
                else if (path.StartsWith("/rest/signals"))
                {
                    handleSignals(context);
                }
                else if (path.StartsWith("/rest/signal/"))
                {
                    handleSignal(context);
                }
                else
                {
                    context.Response.Close();
                }
            }
        }

        private void websocketDisconnected(WebSocketHandler h)
        {
            this.webSockets.Remove(h);
        }

        #region Plugins

        private void handlePlugins(HttpListenerContext context)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(this.model.Plugins, Formatting.Indented);
            byte[] data = Encoding.UTF8.GetBytes(json);

            context.Response.ContentLength64 = data.Length;
            context.Response.OutputStream.Write(data, 0, data.Length);
            context.Response.Close();
        }

        private void handlePlugin(HttpListenerContext context)
        {
            string identifier = context.Request.RawUrl.Substring(("/rest/plugin/").Length);

            PluginState plugin = (PluginState)this.pluginChannel.find(identifier);

            if (plugin != null)
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(plugin, Formatting.Indented);
                byte[] data = Encoding.UTF8.GetBytes(json);

                context.Response.ContentLength64 = data.Length;
                context.Response.OutputStream.Write(data, 0, data.Length);
            }

            context.Response.Close();
        }

        #endregion

        #region Devices

        private void handleDevices(HttpListenerContext context)
        {
			string json = Newtonsoft.Json.JsonConvert.SerializeObject(this.model.Devices, Formatting.Indented);
			byte[] data = Encoding.UTF8.GetBytes(json);

			context.Response.ContentLength64 = data.Length;
			context.Response.OutputStream.Write(data, 0, data.Length);
			context.Response.Close();
        }

        private void handleDevice(HttpListenerContext context)
        {
            string identifier = context.Request.RawUrl.Substring(("/rest/device/").Length);

            DeviceState device = (DeviceState)this.deviceChannel.find(identifier);

            if (device != null)
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(device, Formatting.Indented);
                byte[] data = Encoding.UTF8.GetBytes(json);

                context.Response.ContentLength64 = data.Length;
                context.Response.OutputStream.Write(data, 0, data.Length);
            }

            context.Response.Close();
        }

        #endregion

        #region Signals

        private struct BasicSignal
        {
            private ISignal signal;

            public string Identifier { get { return this.signal.Identifier; } }
            public string SignalType { get { return this.signal.SignalType.UnderlyingSystemType.Name; } }

            public BasicSignal(ISignal s)
            {
                signal = s;
            }
        }

        private void handleSignals(HttpListenerContext context)
        {
            List<BasicSignal> collection = new List<BasicSignal>();

            foreach(ISignal signal in this.model.Signals)
            {
                collection.Add(new BasicSignal(signal));
            }

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(collection, Formatting.Indented);
            byte[] data = Encoding.UTF8.GetBytes(json);

            context.Response.ContentLength64 = data.Length;
            context.Response.OutputStream.Write(data, 0, data.Length);
            context.Response.Close();
        }

        private async void handleSignal(HttpListenerContext context)
        {
            string identifier = context.Request.RawUrl.Substring(("/rest/signal/").Length);

            ISignal signal = (ISignal)this.signalChannel.find(identifier);

            if(signal != null)
            {
                if(context.Request.HttpMethod == "GET")
                {
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(signal, Formatting.Indented);
                    byte[] data = Encoding.UTF8.GetBytes(json);

                    context.Response.ContentLength64 = data.Length;
                    context.Response.OutputStream.Write(data, 0, data.Length);
                }
                else if(context.Request.HttpMethod == "POST")
                {
                    if(context.Request.HasEntityBody)
                    {
                        using (StreamReader sr = new StreamReader(context.Request.InputStream))
                        {
                            string body = await sr.ReadToEndAsync();

                            JsonConvert.PopulateObject(body, signal);
                        }

                        string json = Newtonsoft.Json.JsonConvert.SerializeObject(signal, Formatting.Indented);
                        byte[] data = Encoding.UTF8.GetBytes(json);

                        context.Response.ContentLength64 = data.Length;
                        context.Response.OutputStream.Write(data, 0, data.Length);

                        context.Response.StatusCode = 200; // "OK"
                    }
                    else
                    {
                        context.Response.StatusCode = 400; // "Bad Request"
                    }
                }
            }
            else
            {
                context.Response.StatusCode = 404; // "Not Found"
            }

            context.Response.Close();
        }

        #endregion
    }
}