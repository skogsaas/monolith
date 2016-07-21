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
    class BindingsApi : IApi
    {
        public static string Path
        {
            get
            {
                return "bindings";
            }
        }

        public BindingsApi()
        {

        }

        public void handle(HttpListenerContext context)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(Bindings.Manager.Types, Formatting.Indented);
            byte[] data = Encoding.UTF8.GetBytes(json);

            context.Response.ContentLength64 = data.Length;
            context.Response.OutputStream.Write(data, 0, data.Length);
            context.Response.Close();
        }
    }
}
