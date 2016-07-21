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
            register(DeviceApi.Path, new DeviceApi());
            register(DevicesApi.Path, new DevicesApi());
            register(SignalApi.Path, new SignalApi());
			register(SignalsApi.Path, new SignalsApi());
            register(BindingsApi.Path, new BindingsApi());

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
    }
}