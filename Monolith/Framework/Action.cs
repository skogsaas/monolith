using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public class Action<R, P> : IAction
    {
        public delegate R Handler(P parameter);
        
        private Handler handler;

        public string Method { get; private set; }

        public Action(IActionContainer container, string method, Handler handler = null)
        {
            this.Method = method;
            this.handler = handler;

            container.addAction(this);
        }

        public R Execute(P parameter)
        {
            if(this.handler != null)
            {
                return this.handler(parameter);
            }
            else
            {
                return default(R);
            }
        }

        public void SetHandler(Handler handler)
        {
            this.handler = handler;
        }
    }
}
