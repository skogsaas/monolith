using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Monolith.Bindings;

namespace Monolith.Plugins.GenericBindings
{
    public class GenericBindings : PluginBase
    {
        public GenericBindings()
            : base("GenericBindings")
        {
            
        }

        public override void initialize()
        {
            base.initialize();

            Manager.Register(typeof(OneWay));
            Manager.Register(typeof(TwoWay));
        }
    }
}
