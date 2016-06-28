using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public delegate void ObjectEventHandler(IObject obj);

    public interface IObject
    {
        string Identifier { get; }
        List<IAttribute> Attributes { get; }

        event ObjectEventHandler ObjectChanged;
    }
}
