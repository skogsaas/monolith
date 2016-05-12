﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public delegate void AttributeEventHandler(IAttribute a);

    public interface IAttribute
    {
        event AttributeEventHandler AttributeChanging;
        event AttributeEventHandler AttributeChanged;
    }
}