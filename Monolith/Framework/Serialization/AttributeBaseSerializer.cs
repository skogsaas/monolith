using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework.Serialization
{
    public class AttributeBaseSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Framework.IAttributeBase).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Framework.IAttributeBase attr = (Framework.IAttributeBase)value;

            writer.WritePropertyName(attr.Name);

            if (attr.GetAttributeType() == typeof(byte))
                writer.WriteValue((value as Framework.AttributeBase<byte>).Value);
            else if (attr.GetAttributeType() == typeof(bool))
                writer.WriteValue((value as Framework.AttributeBase<bool>).Value);
            else if (attr.GetAttributeType() == typeof(short))
                writer.WriteValue((value as Framework.AttributeBase<short>).Value);
            else if (attr.GetAttributeType() == typeof(ushort))
                writer.WriteValue((value as Framework.AttributeBase<ushort>).Value);
            else if (attr.GetAttributeType() == typeof(int))
                writer.WriteValue((value as Framework.AttributeBase<int>).Value);
            else if (attr.GetAttributeType() == typeof(uint))
                writer.WriteValue((value as Framework.AttributeBase<uint>).Value);
            else if (attr.GetAttributeType() == typeof(long))
                writer.WriteValue((value as Framework.AttributeBase<long>).Value);
            else if (attr.GetAttributeType() == typeof(ulong))
                writer.WriteValue((value as Framework.AttributeBase<ulong>).Value);
            else if (attr.GetAttributeType() == typeof(double))
                writer.WriteValue((value as Framework.AttributeBase<double>).Value);
            else if (attr.GetAttributeType() == typeof(float))
                writer.WriteValue((value as Framework.AttributeBase<float>).Value);
            else if (attr.GetAttributeType() == typeof(string))
                writer.WriteValue((value as Framework.AttributeBase<string>).Value);
            else
            {
                writer.WriteNull();
                writer.WriteComment("Unable to serialize attribute of type " + attr.GetAttributeType().Name);
            }
        }

        
    }
}
