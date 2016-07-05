using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Obelisk.Providers
{
    public class Signals
    {
        private Models.Model model;

        private List<ISignal> signals;

        private struct BasicSignal
        {
            public string Identifier { get; set; }
            public string SignalType { get; set; }
        }

        public Signals(Models.Model m)
        {
            this.model = m;

            this.signals = new List<ISignal>();

            pull();
        }

        public void add(string identifier, string type)
        {
            create(identifier, type);
        }

        private async void pull()
        {
            try
            {
                WebRequest request = WebRequest.Create("http://localhost:8080/rest/signals");
                WebResponse response = await request.GetResponseAsync();

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string data = await reader.ReadToEndAsync();

                    List<BasicSignal> basics = JsonConvert.DeserializeObject<List<BasicSignal>>(data);

                    foreach (BasicSignal basic in basics)
                    {
                        create(basic.Identifier, basic.SignalType);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void create(string identifier, string type)
        {
            if (type == "Boolean")
                this.signals.Add(new Signal<bool>(this.model, identifier));
            else if (type == "Int16")
                this.signals.Add(new Signal<Int16>(this.model, identifier));
            else if (type == "Int32")
                this.signals.Add(new Signal<Int32>(this.model, identifier));
            else if (type == "UInt16")
                this.signals.Add(new Signal<UInt16>(this.model, identifier));
            else if (type == "Int32")
                this.signals.Add(new Signal<UInt32>(this.model, identifier));
            else if (type == "Double")
                this.signals.Add(new Signal<double>(this.model, identifier));
            else if (type == "Float")
                this.signals.Add(new Signal<float>(this.model, identifier));
        }
    }
}
