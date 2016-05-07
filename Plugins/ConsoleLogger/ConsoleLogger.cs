﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Plugins
{
    public class ConsoleLogger : IPlugin
    {
        private Framework.Channel channel;

        public ConsoleLogger()
        {
            
        }

        public void initialize()
        {
            this.channel = Framework.Manager.Instance.create("Logging");
            this.channel.subscribe(typeof(Logging.LogEvent), onEvent);
        }

        private void onEvent(Framework.Channel cha, Framework.IEvent evt)
        {
            Logging.LogEvent e = (Logging.LogEvent)evt;

            if(e != null)
            {
                string line = String.Format("[{0, 0}] [{1, 0}:{2,0}] [{3, 0}] - {4, 0}", 
                    e.Time.ToString("yyyy:MM:dd HH/mm/ss:fff"), 
                    Path.GetFileName(e.FilePath), 
                    e.Line, 
                    e.Member, 
                    e.Message);
                Console.WriteLine(line);
            }
        }
    }
}
