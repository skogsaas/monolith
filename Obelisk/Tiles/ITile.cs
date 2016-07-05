using Obelisk.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obelisk.Tiles
{
    interface ITile
    {
        void setSignal(ISignal signal);
    }
}
