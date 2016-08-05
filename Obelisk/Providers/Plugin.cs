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
    public class Plugin
    {
        public Models.Plugin PluginModel { get; private set; }

        private Models.Model model;

        public Plugin(Models.Model m, string identifier)
        {
            this.model = m;

            this.PluginModel = new Models.Plugin(identifier);

            // Do the initial pull.
            pull();

            this.model.Plugins.Add(this.PluginModel);
        }
        
        private async void pull()
        {
            try
            {
                WebRequest request = WebRequest.Create("http://localhost:8080/rest/plugin/" + this.PluginModel.Identifier);
                request.Method = "GET";

                WebResponse response = await request.GetResponseAsync();

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string data = await reader.ReadToEndAsync();
                    
                    JsonConvert.PopulateObject(data, this);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
            }
        }
    }
}
