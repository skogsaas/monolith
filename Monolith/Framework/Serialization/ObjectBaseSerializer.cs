using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework.Serialization
{
    public class ObjectBaseSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Framework.ObjectBase).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Framework.ObjectBase obj = null;
            
            while(reader.TokenType != JsonToken.EndObject)
            {
                switch(reader.TokenType)
                {
                    case JsonToken.StartObject:
                        reader.Read();
                        break;

                    case JsonToken.PropertyName:
                        {
                            if (reader.Path == "Identifier")
                            {
                                obj = (Framework.ObjectBase)Activator.CreateInstance(objectType, reader.ReadAsString());
                            }
                            else if (reader.Path == "Type")
                            {
                                reader.Skip();
                            }
                            else
                            {
                                try
                                {
                                    IAttribute a = obj.getAttributes().Single(at => at.Name == (string)reader.Value);

                                    readAttribute(reader, a);
                                }
                                catch(Exception ex)
                                {
                                    reader.Skip();
                                }
                            }

                            reader.Read();
                        }
                        break;

                    case JsonToken.StartArray:
                        reader.Skip();
                        reader.Read();
                        break;

                    default:
                        reader.Skip();
                        reader.Read();
                        break;
                }
            }

            return obj;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Framework.IObject obj = (Framework.IObject)value;

            writer.WriteStartObject();

            writer.WritePropertyName("Identifier");
            writer.WriteValue(obj.Identifier);

            writer.WritePropertyName("Type");
            writer.WriteValue(obj.Type);

            foreach(Framework.IAttribute attr in obj.getAttributes())
            {
                try
                {
                    serializer.Serialize(writer, attr);
                }
                catch(Exception ex)
                {
                    string msg = ex.Message;
                }
            }

            writer.WriteEndObject();
        }

        private void readAttribute(JsonReader reader, IAttribute a)
        {
            if (a.GetAttributeType() == typeof(byte))
            {
                Framework.AttributeBase<byte> attr = (Framework.AttributeBase<byte>)a;
                int? v = reader.ReadAsInt32();

                if (v.HasValue)
                    attr.Value = (byte)v.Value;
            }
            else if (a.GetAttributeType() == typeof(bool))
            {
                Framework.AttributeBase<bool> attr = (Framework.AttributeBase<bool>)a;
                bool? v = reader.ReadAsBoolean();

                if (v.HasValue)
                    attr.Value = (bool)v.Value;
            }
            else if (a.GetAttributeType() == typeof(short))
            {
                Framework.AttributeBase<short> attr = (Framework.AttributeBase<short>)a;
                int? v = reader.ReadAsInt32();

                if (v.HasValue)
                    attr.Value = (short)v.Value;
            }
            else if (a.GetAttributeType() == typeof(ushort))
            {
                Framework.AttributeBase<ushort> attr = (Framework.AttributeBase<ushort>)a;
                int? v = reader.ReadAsInt32();

                if (v.HasValue)
                    attr.Value = (ushort)v.Value;
            }
            else if (a.GetAttributeType() == typeof(int))
            {
                Framework.AttributeBase<int> attr = (Framework.AttributeBase<int>)a;
                int? v = reader.ReadAsInt32();

                if (v.HasValue)
                    attr.Value = (int)v.Value;
            }
            else if (a.GetAttributeType() == typeof(uint))
            {
                Framework.AttributeBase<uint> attr = (Framework.AttributeBase<uint>)a;
                int? v = reader.ReadAsInt32();

                if (v.HasValue)
                    attr.Value = (uint)v.Value;
            }
            else if (a.GetAttributeType() == typeof(long))
            {
                Framework.AttributeBase<long> attr = (Framework.AttributeBase<long>)a;
                int? v = reader.ReadAsInt32();

                if (v.HasValue)
                    attr.Value = (long)v.Value;
            }
            else if (a.GetAttributeType() == typeof(ulong))
            {
                Framework.AttributeBase<ulong> attr = (Framework.AttributeBase<ulong>)a;
                int? v = reader.ReadAsInt32();

                if (v.HasValue)
                    attr.Value = (ulong)v.Value;
            }
            else if (a.GetAttributeType() == typeof(double))
            {
                Framework.AttributeBase<double> attr = (Framework.AttributeBase<double>)a;
                double? v = reader.ReadAsDouble();

                if (v.HasValue)
                    attr.Value = (double)v.Value;
            }
            else if (a.GetAttributeType() == typeof(float))
            {
                Framework.AttributeBase<float> attr = (Framework.AttributeBase<float>)a;
                decimal? v = reader.ReadAsDecimal();

                if (v.HasValue)
                    attr.Value = (float)v.Value;
            }
            else if (a.GetAttributeType() == typeof(string))
            {
                Framework.AttributeBase<string> attr = (Framework.AttributeBase<string>)a;
                attr.Value = reader.ReadAsString();
            }
            else if(typeof(IAttributeCollection).IsAssignableFrom(a.GetType()))
            {
                IAttributeCollection parent = (IAttributeCollection)a;

                reader.Read(); // Enter the collection object

                // Try to find the first property
                while (reader.TokenType != JsonToken.PropertyName && reader.TokenType != JsonToken.EndObject)
                {
                    reader.Read();
                }

                while(reader.TokenType != JsonToken.EndObject)
                {
                    IAttribute child = (IAttribute)Activator.CreateInstance(a.GetAttributeType(), parent, reader.Value);
                    readAttribute(reader, child);

                    reader.Read();
                }
            }
            else
            {
                reader.Skip();
            }
        }
    }
}
