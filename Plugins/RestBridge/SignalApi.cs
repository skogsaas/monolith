﻿using Monolith.Framework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Plugins.REST
{
	class SignalApi : IApi
	{
		private Channel channel;

		public static string Path
		{
			get
			{
				return "signal";
			}
		}

		public SignalApi()
		{
			this.channel = Framework.Manager.Instance.create(Signals.Constants.Channel);
		}

		public void handle(HttpListenerContext context)
		{
			string identifier = context.Request.RawUrl.Substring(("/rest/signal/").Length);

			Signals.ISignal signal = (Signals.ISignal)this.channel.find(identifier);

			if (signal != null)
			{
				string json = Newtonsoft.Json.JsonConvert.SerializeObject(signal, Formatting.Indented);
				byte[] data = Encoding.UTF8.GetBytes(json);

				context.Response.ContentLength64 = data.Length;
				context.Response.OutputStream.Write(data, 0, data.Length);
			}

			context.Response.Close();
		}
	}
}
