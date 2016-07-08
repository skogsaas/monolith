using Obelisk.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Sockets;
using Windows.Storage.Streams;

namespace Obelisk.Providers
{
    class Provider
    {
        private MessageWebSocket websocket;

        public Model Model { get; private set; }

        private Signals signals;
        private Bindings bindings;

        public Provider(Model model)
        {
            this.Model = model;
            this.signals = new Signals(this.Model);
            this.bindings = new Bindings(this.Model);

            this.websocket = new MessageWebSocket();
            this.websocket.Control.MessageType = SocketMessageType.Utf8;
            this.websocket.MessageReceived += Websocket_MessageReceived;

            connect();

            getPlugins();
            getDevices();
        }

        private async void connect()
        {
            await this.websocket.ConnectAsync(new Uri("ws://localhost:8080/rest"));

            /*
            DataWriter w = new DataWriter(this.websocket.OutputStream);
            w.UnicodeEncoding = Windows.Storage.Streams.UnicodeEncoding.Utf8;

            w.WriteString("Hello World!");

            await w.StoreAsync();
            */
        }

        private void Websocket_MessageReceived(MessageWebSocket sender, MessageWebSocketMessageReceivedEventArgs args)
        {
            using (DataReader reader = args.GetDataReader())
            {
                string data = reader.ReadString(reader.UnconsumedBufferLength);

                Messages.MessageBase peek = JsonConvert.DeserializeObject<Messages.MessageBase>(data);
                
                if(peek.Type == typeof(Messages.SignalEvent).Name)
                {
                    Messages.SignalEvent msg = JsonConvert.DeserializeObject<Messages.SignalEvent>(data);

                    this.signals.add(msg.Signal, msg.SignalType);
                }
            }
        }

        private async void getPlugins()
        {
            try
            {
                WebRequest request = WebRequest.Create("http://localhost:8080/rest/plugins");
                WebResponse response = await request.GetResponseAsync();

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string data = await reader.ReadToEndAsync();

                    this.Model.Plugins = JsonConvert.DeserializeObject<ObservableCollection<Plugin>>(data);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void getDevices()
        {
            try
            {
                WebRequest request = WebRequest.Create("http://localhost:8080/rest/devices");
                WebResponse response = await request.GetResponseAsync();

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string data = await reader.ReadToEndAsync();

                    this.Model.Devices = JsonConvert.DeserializeObject<ObservableCollection<Device>>(data);
                }
            }
            catch (Exception ex)
            {

            }
        }
        
    }
}
