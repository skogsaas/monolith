using Monolith.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Signals
{
    public class Signal<T> : Framework.ObjectBase, ISignal
    {
        private AttributeBase<T> state;

        public T State
        {
            get
            {
                return state.Value;
            }

            set
            {
                this.state.Value = value;
            }
        }

        public T Minimum { get; set; }
        public T Maximum { get; set; }
        public Dictionary<T, string> Values { get; private set; }

        public Type SignalType
        {
            get
            {
                return typeof(T);
            }
        }

        public Signal(string identifier)
            : base(identifier)
        {
            this.state = new AttributeBase<T>(this);
            this.Values = new Dictionary<T, string>();
        }
    }
}
