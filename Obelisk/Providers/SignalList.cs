using System;
using System.Collections.Generic;

namespace Obelisk.Providers
{
    public class SignalList : GenericList
    {
        private Dictionary<string, Type> types;
        private List<Signal<dynamic>> signals;

        public SignalList(Models.Model m)
            : base(m, "signals")
        {
            this.types = new Dictionary<string, Type>();
            this.signals = new List<Signal<dynamic>>();

            this.types.Add("Signal.Boolean", typeof(Signal<bool>));
            this.types.Add("Signal.Int16", typeof(Signal<short>));
            this.types.Add("Signal.Int32", typeof(Signal<int>));
            this.types.Add("Signal.Int64", typeof(Signal<long>));
            this.types.Add("Signal.UInt16", typeof(Signal<ushort>));
            this.types.Add("Signal.UInt32", typeof(Signal<uint>));
            this.types.Add("Signal.UInt64", typeof(Signal<ulong>));
            this.types.Add("Signal.Double", typeof(Signal<double>));
            this.types.Add("Signal.Float", typeof(Signal<float>));
        }

        protected override void create(string identifier, string type)
        {
            if (this.types.ContainsKey(type))
            {
                Activator.CreateInstance(this.types[type], new object[] { this.model, identifier });
            }
        }
    }
}
