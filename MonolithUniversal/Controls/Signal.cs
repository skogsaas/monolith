using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MonolithUniversal.Controls
{
    public class Signal
    {
        public Models.ISignal Model { get; private set; }

        public Signal(Models.ISignal model)
        {
            this.Model = model;
        }

        public async void refresh()
        {
            try
            {
                WebRequest request = WebRequest.Create("http://localhost:8080/rest/signal/" + this.Model.Identifier);
                WebResponse response = await request.GetResponseAsync();

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string data = await reader.ReadToEndAsync();

                    if (data.Length > 0)
                    {
                        JsonConvert.PopulateObject(data, this.Model);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
