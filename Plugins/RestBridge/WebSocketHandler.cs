using Monolith.Signals;
using Newtonsoft.Json;
using RestBridge.Messages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monolith.Plugins.REST
{
    class WebSocketHandler
    {
        private WebSocket socket;
        private CancellationTokenSource token;

        public delegate void ClosedHandler(WebSocketHandler h);
        public event ClosedHandler Disconnected;

        public WebSocketHandler(WebSocket s)
        {
            this.socket = s;
            this.token = new CancellationTokenSource();

            Task.Factory.StartNew(receive, this.token.Token);
        }

        private async void Signals_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach(object o in e.NewItems)
            {
                SignalEvent msg = new SignalEvent();
                msg.Signal = (o as ISignal).Identifier;
                msg.SignalType = (o as ISignal).SignalType.Name;

                string data = JsonConvert.SerializeObject(msg);

                ArraySegment<byte> buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(data));

                await this.socket.SendAsync(buffer, WebSocketMessageType.Text, true, token.Token);
            }
        }

        ~WebSocketHandler()
        {
            this.token.Cancel();
        }

        private async void receive()
        {
            try
            {
                ArraySegment<byte> buffer = WebSocket.CreateServerBuffer(1024 * 64);

                while (!this.token.Token.IsCancellationRequested)
                {
                    WebSocketReceiveResult r = await this.socket.ReceiveAsync(buffer, this.token.Token);

                    if (r.CloseStatus == null)
                    {
                        process(Encoding.UTF8.GetString(buffer.Array, 0, r.Count));
                    }
                    else
                    {
                        this.Disconnected?.Invoke(this);

                        break;
                    }
                }
            }
            catch(Exception ex)
            {
                this.Disconnected?.Invoke(this);
            }
        }

        private void process(string data)
        {
            try
            {
                MessageBase msg = JsonConvert.DeserializeObject<MessageBase>(data);

                /* TODO
                if(msg.Type == typeof(SignalEvent).Name)
                {

                }
                else
                {

                }
                */
            }
            catch(Exception ex)
            {
                Logging.Logger.Error("Unable to deserialize a message with the data: " + data);
            }
        }
    }
}
