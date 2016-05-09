using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monolith.Plugins;
using Monolith.Framework;
using System.IO;
using Newtonsoft.Json;
using System.Net;

namespace Monolith.Plugins.REST
{
    public class RestBridge : IPlugin
    {
        private Channel channel;

        private HttpListener listener;

        public RestBridge()
        {

        }

        public void initialize()
        {
            loadConfiguration();

            this.listener = new HttpListener();
            this.listener.Prefixes.Add("http://+:8080/rest/");
            this.listener.Start();
            listen();
        }

        private void loadConfiguration()
        {
            try
            {
                List<string> channels = JsonConvert.DeserializeObject<List<string>>(File.ReadAllText("RestBridge.json"));
            }
            catch(Exception ex)
            {
                string s = ex.ToString();
            }
        }

        private async void listen()
        {
            while(true)
            {
                HttpListenerContext context = await this.listener.GetContextAsync();
                await Task.Factory.StartNew(() => process(context));
            }
        }

        private void process(HttpListenerContext context)
        {
            string path = context.Request.RawUrl;

            if(path.StartsWith("/rest/plugins"))
            {

            }
            else if (path.StartsWith("/rest/devices"))
            {
                string t = "abc";
            }
            else
            {
                context.Response.Close();
            }
        }

        private void plugins(HttpListenerContext context)
        {

        }
    }
}