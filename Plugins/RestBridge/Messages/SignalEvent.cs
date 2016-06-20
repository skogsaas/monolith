using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestBridge.Messages
{
    class SignalEvent : MessageBase
    {
        public string Signal { get; set; }
        public string SignalType { get; set; }

        public SignalEvent()
        {
            this.Type = this.GetType().Name;
        }
    }
}
