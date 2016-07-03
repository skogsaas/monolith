using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Plugins
{
    public class ConsoleLogger : PluginBase
    {
        private Framework.Channel logChannel;

        public ConsoleLogger()
            : base("ConsoleLogger")
        {
            
        }

        public override void initialize()
        {
            base.initialize();

            this.logChannel = Framework.Manager.Instance.create("Logging");
            this.logChannel.subscribe(typeof(Logging.LogEvent), onEvent);
        }

        private void onEvent(Framework.Channel cha, Framework.IEvent evt)
        {
            Logging.LogEvent e = (Logging.LogEvent)evt;

            if(e != null)
            {
                string line = String.Format("[{0, 0}] [{1, 0}:{2,0}] [{3, 0}] - {4, 0}", 
                    e.Time.ToString("yyyy/MM/dd HH:mm:ss.fff"), 
                    Path.GetFileName(e.FilePath), 
                    e.Line, 
                    e.Member, 
                    e.Message);
                Console.WriteLine(line);
            }
        }
    }
}
