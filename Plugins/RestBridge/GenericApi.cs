using Monolith.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Plugins.REST
{
    public class GenericApi<T> : IApi
    {
        private Channel channel;
        private string path;

        public GenericApi(Channel c, string p)
        {
            this.channel = c;
            this.path = p;
        }

        public async void handle(HttpListenerContext context)
        {
            string identifier = context.Request.RawUrl.Substring((this.path).Length);

            IObject obj = this.channel.find(identifier);

            if (obj != null)
            {
                if (context.Request.HttpMethod == "GET")
                {
                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(obj, Formatting.Indented);
                    byte[] data = Encoding.UTF8.GetBytes(json);

                    context.Response.ContentLength64 = data.Length;
                    context.Response.OutputStream.Write(data, 0, data.Length);
                }
                /*
                else if (context.Request.HttpMethod == "POST")
                {
                    if (context.Request.HasEntityBody)
                    {
                        using (StreamReader sr = new StreamReader(context.Request.InputStream))
                        {
                            string body = await sr.ReadToEndAsync();

                            JsonConvert.PopulateObject(body, signal);
                        }

                        string json = Newtonsoft.Json.JsonConvert.SerializeObject(signal, Formatting.Indented);
                        byte[] data = Encoding.UTF8.GetBytes(json);

                        context.Response.ContentLength64 = data.Length;
                        context.Response.OutputStream.Write(data, 0, data.Length);

                        context.Response.StatusCode = 200; // "OK"
                    }
                    else
                    {
                        context.Response.StatusCode = 400; // "Bad Request"
                    }
                }
                */
            }
            else
            {
                context.Response.StatusCode = 404; // "Not Found"
            }

            context.Response.Close();
        }
    }
}
