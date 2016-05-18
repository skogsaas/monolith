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
    public class Signal<T> : ISignal
    {
        public string Identifier { get; private set; }
        public T State { get; set; }

        public Signal(string identifier)
        {
            this.Identifier = identifier;

            update();
        }

        public async void update()
        {
            WebRequest request = WebRequest.Create("http://localhost:8080/rest/signal/" + this.Identifier);
            WebResponse response = await request.GetResponseAsync();

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                string data = await reader.ReadToEndAsync();

                JsonConvert.PopulateObject(data, this);
            }
        }
    }
}
