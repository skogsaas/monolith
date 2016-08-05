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
    public class GenericList
    {
        protected Models.Model model;
        protected string path;

        public GenericList(Models.Model m, string p)
        {
            this.model = m;
            this.path = p;

            pull();
        }

        private async void pull()
        {
            try
            {
                WebRequest request = WebRequest.Create("http://localhost:8080/rest/" + this.path + "/");
                WebResponse response = await request.GetResponseAsync();

                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    string data = await reader.ReadToEndAsync();

                    List<TypeIdentifier> basics = JsonConvert.DeserializeObject<List<TypeIdentifier>>(data);

                    foreach (TypeIdentifier type in basics)
                    {
                        create(type.Identifier, type.Type);
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected virtual void create(string identifier, string type)
        {
            throw new NotImplementedException();
        }

        private struct TypeIdentifier
        {
            public string Identifier { get; set; }
            public string Type { get; set; }
        }
    }
}
