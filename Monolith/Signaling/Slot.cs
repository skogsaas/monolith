using Monolith.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Signaling
{
    public class Slot<T> : Framework.ObjectBase, ISignaling
        where T : IConvertible
    {
        public AttributeBase<T> State { get; private set; }
        public AttributeBase<T> Minimum { get; private set; }
        public AttributeBase<T> Maximum { get; private set; }
        //public AttributeBase<Dictionary<T, string>> Values { get; private set; }

        public Slot(string identifier)
            : base(identifier)
        {
            this.Type = "Slot." + typeof(T).Name;

            this.State = new AttributeBase<T>(this, "State");
            this.Minimum = new AttributeBase<T>(this, "Minimum");
            this.Maximum = new AttributeBase<T>(this, "Maximum");
            //this.Values = new AttributeBase<Dictionary<T, string>>(this, "Values");
        }
    }
}
