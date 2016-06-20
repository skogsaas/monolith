using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MonolithUniversal.Providers
{
    public class Signal<T> : ISignal
    {
        public Models.Signal<T> SignalModel { get; private set; }

        private Models.Model model;
        private bool signalLock = false;

        public Signal(Models.Model m, string identifier)
        {
            this.model = m;

            this.SignalModel = new Models.Signal<T>(identifier);
            this.SignalModel.PropertyChanged += SignalModel_PropertyChanged;

            this.model.Signals.Add(this.SignalModel);

            // Do the initial pull.
            pull();
        }

        private void SignalModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if(!this.signalLock)
            {
                if (e.PropertyName == "State")
                {
                    push();
                }
            }
        }

        private async void pull()
        {
            try
            {
                WebRequest request = WebRequest.Create("http://localhost:8080/rest/signal/" + this.SignalModel.Identifier);
                request.Method = "GET";

                WebResponse response = await request.GetResponseAsync();

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string data = await reader.ReadToEndAsync();

                    this.signalLock = true;
                    JsonConvert.PopulateObject(data, this);
                    this.signalLock = false;
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                this.signalLock = false;
            }
        }

        private async void push()
        {
            try
            {
                HttpWebRequest request = HttpWebRequest.CreateHttp("http://localhost:8080/rest/signal/" + this.SignalModel.Identifier);
                request.Method = "POST";

                using (StreamWriter writer = new StreamWriter(await request.GetRequestStreamAsync()))
                {
                    string data = JsonConvert.SerializeObject(this.SignalModel);
                    await writer.WriteAsync(data);
                }

                HttpWebResponse response = (HttpWebResponse) await request.GetResponseAsync();
                HttpStatusCode status = response.StatusCode;
            }
            catch (Exception ex)
            {

            }
        }
    }
}
