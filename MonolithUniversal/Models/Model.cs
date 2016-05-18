using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MonolithUniversal.Models
{
    public class Model
    {
        public ObservableCollection<Plugin> Plugins { get; set; }
        public ObservableCollection<Device> Devices { get; set; }
        public ObservableCollection<ISignal> Signals { get; set; }

        public Model()
        {
            this.Plugins = new ObservableCollection<Plugin>();
            this.Devices = new ObservableCollection<Device>();
            this.Signals = new ObservableCollection<ISignal>();

            getPlugins();
            getDevices();
            getSignals();
        }

        private async void getPlugins()
        {
            WebRequest request = WebRequest.Create("http://localhost:8080/rest/plugins");
            WebResponse response = await request.GetResponseAsync();

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string data = await reader.ReadToEndAsync();

                this.Plugins = JsonConvert.DeserializeObject<ObservableCollection<Plugin>>(data);
            }
        }

        private async void getDevices()
        {
            WebRequest request = WebRequest.Create("http://localhost:8080/rest/devices");
            WebResponse response = await request.GetResponseAsync();

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string data = await reader.ReadToEndAsync();

                this.Devices = JsonConvert.DeserializeObject<ObservableCollection<Device>>(data);
            }
        }

        #region Signals

        private async void getSignals()
        {
            WebRequest request = WebRequest.Create("http://localhost:8080/rest/signals");
            WebResponse response = await request.GetResponseAsync();

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string data = await reader.ReadToEndAsync();

                List<BasicSignal> basics = JsonConvert.DeserializeObject<List<BasicSignal>>(data);

                foreach(BasicSignal basic in basics)
                {
                    if (basic.SignalType == "Bool")
                        this.Signals.Add(new Signal<bool>(basic.Identifier));
                    else if (basic.SignalType == "Int16")
                        this.Signals.Add(new Signal<Int16>(basic.Identifier));
                    else if (basic.SignalType == "Int32")
                        this.Signals.Add(new Signal<Int32>(basic.Identifier));
                    else if (basic.SignalType == "UInt16")
                        this.Signals.Add(new Signal<UInt16>(basic.Identifier));
                    else if (basic.SignalType == "Int32")
                        this.Signals.Add(new Signal<UInt32>(basic.Identifier));
                    else if (basic.SignalType == "Double")
                        this.Signals.Add(new Signal<double>(basic.Identifier));
                    else if (basic.SignalType == "Float")
                        this.Signals.Add(new Signal<float>(basic.Identifier));
                }
            }
        }

        private struct BasicSignal
        {
            public string Identifier { get; set; }
            public string SignalType { get; set; }
        }

        #endregion
    }
}
