using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MonolithUniversal.Models
{
    public class Model
    {
        public List<Plugin> Plugins { get; set; }
        private List<Device> Devices { get; set; }
        private List<ISignal> Signals { get; set; }

        public Model()
        {
            this.Plugins = new List<Plugin>();
            this.Devices = new List<Device>();
            this.Signals = new List<ISignal>();

            getPlugins();
            getDevices();
        }

        private async void getPlugins()
        {
            WebRequest request = WebRequest.Create("http://localhost:8080/rest/plugins");
            WebResponse response = await request.GetResponseAsync();

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string data = await reader.ReadToEndAsync();

                this.Plugins = JsonConvert.DeserializeObject<List<Plugin>>(data);
            }
        }

        private async void getDevices()
        {
            WebRequest request = WebRequest.Create("http://localhost:8080/rest/devices");
            WebResponse response = await request.GetResponseAsync();

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string data = await reader.ReadToEndAsync();

                this.Devices = JsonConvert.DeserializeObject<List<Device>>(data);
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
            }
        }

        private struct BasicSignal
        {
            public Type SignalType { get; set; }
        }

        #endregion
    }
}
