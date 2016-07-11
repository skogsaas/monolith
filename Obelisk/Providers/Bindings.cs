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
    public class Bindings
    {
        private Models.Model model;

        public Bindings(Models.Model m)
        {
            this.model = m;
        }

        private async void pull()
        {
            try
            {
                WebRequest request = WebRequest.Create("http://localhost:8080/rest/bindings");
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
    }
}
