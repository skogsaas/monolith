using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yr.Network; 

namespace Yr.Network.Test.Objects
{
    public class SomeClass : NetworkBase<SomeClass> 
    {
        public string to;

        public string from;

        public string heading;

        public string body; 
    }
}
