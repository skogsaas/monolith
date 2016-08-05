using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Logging
{
    public class LogEvent : Framework.EventBase
    {
        public enum Severity
        {
            Trace,
            Info,
            Warning,
            Error,
            Fatal
        }

        public DateTime Time { get; set; }
        public string Member { get; set; }
        public string FilePath { get; set; }
        public int Line { get; set; }
        public Severity Level { get; set; }
        public string Message { get; set; }

        public LogEvent()
            : base()
        {

        }
    }
}
