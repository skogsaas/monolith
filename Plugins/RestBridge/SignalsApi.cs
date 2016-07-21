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
	class SignalsApi : IApi
	{
		private Channel channel;

		private Dictionary<string, Signals.ISignal> signals;

		public static string Path
		{
			get
			{
				return "signals";
			}
		}

		public SignalsApi()
		{
			this.channel = Framework.Manager.Instance.create(Signals.Constants.Channel);
			this.signals = new Dictionary<string, Signals.ISignal>();
			this.channel.subscribe(typeof(Signals.ISignal), this.onObject);
		}

        private struct BasicSignal
        {
            private Signals.ISignal signal;

            public string Identifier { get { return this.signal.Identifier; } }
            public string SignalType { get { return this.signal.SignalType.UnderlyingSystemType.Name; } }

            public BasicSignal(Signals.ISignal s)
            {
                signal = s;
            }
        }

        public void handle(HttpListenerContext context)
		{
            List<BasicSignal> collection = new List<BasicSignal>();

            foreach (KeyValuePair<string, Signals.ISignal> pair in this.signals)
            {
                collection.Add(new BasicSignal(pair.Value));
            }

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(collection, Formatting.Indented);
            byte[] data = Encoding.UTF8.GetBytes(json);

            context.Response.ContentLength64 = data.Length;
            context.Response.OutputStream.Write(data, 0, data.Length);
            context.Response.Close();
        }

		private void onObject(Channel c, IObject obj)
		{
			if (typeof(Signals.ISignal).IsAssignableFrom(obj.GetType()))
			{
				this.signals[obj.Identifier] = (Signals.ISignal)obj;
			}
		}
	}
}
