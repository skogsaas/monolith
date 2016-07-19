using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Plugins.REST
{
    interface IApi
    {
        void handle(HttpListenerContext context);
    }
}
