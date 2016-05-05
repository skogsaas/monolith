using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public class Event<T>
    {
        private T _value;

        public T Value
        {
            get { return this._value; }
            set { this._value = value; }
        }
    }
}
