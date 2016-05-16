using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithUniversal.Models
{
    public class Signal<T>
    {
        public string Identifier { get; private set; }
        public T State { get; set; }

        public Signal(string identifier)
        {
            this.Identifier = identifier;
        }
    }
}
