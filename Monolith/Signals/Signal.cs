using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Signals
{
    public class Signal<T> : ISignal, Framework.IObject
    {
        protected T state;

        public T State
        {
            get
            {
                return this.state;
            }

            set
            {
                if(!this.state.Equals(value))
                {
                    this.SignalChanging?.Invoke(this);
                    this.state = value;
                    this.SignalChanged?.Invoke(this);
                }
            }
        }

        public string Identifier
        {
            get;
            private set;
        }

        public event SignalEventHandler SignalChanging;
        public event SignalEventHandler SignalChanged;

        public Signal(string identifier)
        {
            this.Identifier = identifier;
        }

        private Signal()
        {
        }
    }
}
