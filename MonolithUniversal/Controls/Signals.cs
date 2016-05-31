using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonolithUniversal.Controls
{
    public class Signals
    {
        private Models.Model model;

        private List<Signal> signals;

        public Signals(Models.Model m)
        {
            this.model = m;

            this.signals = new List<Signal>();
        }
    }
}
