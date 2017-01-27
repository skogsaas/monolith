using Skogsaas.Legion;
using System;
using System.IO;

namespace Skogsaas.Monolith.Plugins
{
    public class ConsoleLogger : PluginBase
    {
        private Channel logChannel;

        public ConsoleLogger()
            : base(nameof(ConsoleLogger))
        {
            
        }

        public override void initialize()
        {
            base.initialize();

            this.logChannel = Manager.Create(Monolith.Logging.Constants.Channel);
            this.logChannel.SubscribePublish(typeof(Logging.LogEvent), onEvent);
        }

        private void onEvent(Channel channel, IEvent evt)
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
