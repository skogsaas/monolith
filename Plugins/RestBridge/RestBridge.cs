using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monolith.Plugins;
using Monolith.Framework;
using Monolith.Signals;
using System.Net;
using Monolith.Devices;
using System.Net.WebSockets;

namespace Monolith.Plugins.REST
{
    public class RestBridge : PluginBase
    {
        private HttpListener listener;

        List<WebSocketHandler> webSockets;
        Dictionary<string, IApi> handlers;

        public RestBridge()
            : base("RestBridge")
        {
            this.webSockets = new List<WebSocketHandler>();
            this.handlers = new Dictionary<string, IApi>();
        }

        public override void initialize()
        {
            base.initialize();

            this.listener = new HttpListener();
            this.listener.Prefixes.Add("http://+:8080/rest/");
            this.listener.Start();

            register(PluginApi.Path, new PluginApi());
            register(PluginsApi.Path, new PluginsApi());
			register(SignalApi.Path, new SignalApi());
			register(SignalsApi.Path, new SignalsApi());

            Task.Factory.StartNew(() => listen());
        }

        private void register(string path, IApi api)
        {
            this.handlers["/rest/" + path + "/"] = api;
        }

        private async void listen()
        {
            while(true)
            {
                HttpListenerContext context = await this.listener.GetContextAsync();
                await Task.Factory.StartNew(() => process(context));
            }
        }

        private void process(HttpListenerContext context)
        {
            if(context.Request.IsWebSocketRequest)
            {
                /*
                HttpListenerWebSocketContext c = await context.AcceptWebSocketAsync(null);

                WebSocketHandler h = new WebSocketHandler(c.WebSocket, this.model);
                h.Disconnected += websocketDisconnected;
                this.webSockets.Add(h);
                */
            }
            else
            {
                string path = context.Request.RawUrl;

                foreach(KeyValuePair<string, IApi> pair in this.handlers)
                {
                    if(path.StartsWith(pair.Key))
                    {
                        pair.Value.handle(context);
                        break;
                    }
                }
            }
        }

        private void websocketDisconnected(WebSocketHandler h)
        {
            this.webSockets.Remove(h);
        }

        /*
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
        */
    }
}