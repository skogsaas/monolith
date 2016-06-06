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
        public delegate bool AcceptHandler(T value);
        private AcceptHandler stateHandler = null;

        public AttributeBase<T> InnerState { get; private set; }

        public T State
        {
            get
            {
                return this.InnerState.Value;
            }

            set
            {
                if(this.stateHandler(value))
                {
                    this.InnerState.Value = value;
                }
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

        public Signal(string identifier, AcceptHandler handler)
            : base(identifier)
        {
            this.InnerState = new AttributeBase<T>(this);
            this.Values = new Dictionary<T, string>();

            this.stateHandler = handler;
        }

        public static bool AllwaysAccept(T value)
        {
            return true;
        }

        public static bool NeverAccept(T value)
        {
            return false;
        }
    }
}
