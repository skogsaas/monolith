using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework
{
    public class Byte : AttributeBase<byte>
    {
        public Byte(IAttributeContainer container, string name) : base(container, name)
        {
        }
    }

    public class Short : AttributeBase<short>
    {
        public Short(IAttributeContainer container, string name) : base(container, name)
        {
        }
    }

    public class UShort : AttributeBase<ushort>
    {
        public UShort(IAttributeContainer container, string name) : base(container, name)
        {
        }
    }

    public class Int : AttributeBase<int>
    {
        public Int(IAttributeContainer container, string name) : base(container, name)
        {
        }
    }

    public class UInt : AttributeBase<uint>
    {
        public UInt(IAttributeContainer container, string name) : base(container, name)
        {
        }
    }

    public class Long : AttributeBase<long>
    {
        public Long(IAttributeContainer container, string name) : base(container, name)
        {
        }
    }

    public class ULong : AttributeBase<ulong>
    {
        public ULong(IAttributeContainer container, string name) : base(container, name)
        {
        }
    }

    public class Double : AttributeBase<double>
    {
        public Double(IAttributeContainer container, string name) : base(container, name)
        {
        }
    }

    public class Float : AttributeBase<double>
    {
        public Float(IAttributeContainer container, string name) : base(container, name)
        {
        }
    }

    public class String : AttributeBase<string>
    {
        public String(IAttributeContainer container, string name) : base(container, name)
        {
        }
    }
}
