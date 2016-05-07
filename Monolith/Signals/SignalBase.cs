using Monolith.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Signals
{
    internal class SignalBase<T> : Framework.ObjectBase, ISignal
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

        public SignalBase(string identifier)
            : base(identifier)
        {
            this.state = new AttributeBase<T>(this);
        }
    }
}
