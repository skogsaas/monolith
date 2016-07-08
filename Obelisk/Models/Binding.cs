using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obelisk.Models
{
    public class Binding
    {
        public string Type { get; set; }

        public ISignal From { get; set; }
        public ISignal To { get; set; }
    }
}
