using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public class EventBase : IEvent
    {
        public string Type { get; set; }

        public EventBase()
        {
            this.Type = this.GetType().Name;
        }
    }
}
