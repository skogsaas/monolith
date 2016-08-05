using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public class Action<P, R> : IAction
    {
        public delegate R Handler(P parameter);
        
        private Handler handler;

        public string Method { get; private set; }

        public Action(IActionContainer container, string m, Handler h)
        {
            this.Method = m;
            this.handler = h;

            container.addAction(this);
        }

        public R Execute(P parameter)
        {
            return this.handler(parameter);
        }
    }
}
