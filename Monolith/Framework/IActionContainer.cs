﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public interface IActionContainer
    {
        void addAction(IAction a);
    }
}
