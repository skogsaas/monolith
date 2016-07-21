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

		public void handle(HttpListenerContext context)
		{
			string json = Newtonsoft.Json.JsonConvert.SerializeObject(this.signals, Formatting.Indented);
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
