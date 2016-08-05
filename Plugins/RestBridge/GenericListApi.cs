using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Monolith.Plugins.REST
{
    public class GenericListApi<T> : IApi
    {
        private Framework.Channel channel;

        public GenericListApi(Framework.Channel c)
        {
            this.channel = c;
        }

        public void handle(HttpListenerContext context)
        {
            List<T> objects = this.channel.findOfType<T>();

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(objects, Formatting.Indented);
            byte[] data = Encoding.UTF8.GetBytes(json);

            context.Response.ContentLength64 = data.Length;
            context.Response.OutputStream.Write(data, 0, data.Length);
            context.Response.Close();
        }
    }
}
