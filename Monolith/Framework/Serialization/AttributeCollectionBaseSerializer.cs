using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Framework.Serialization
{
    public class AttributeCollectionBaseSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(Framework.IAttributeCollection).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            Framework.IAttributeCollection collection = (Framework.IAttributeCollection)value;

            writer.WritePropertyName(collection.Name);
            writer.WriteStartObject();

            foreach (KeyValuePair<string, IAttribute> pair in collection.GetAttributes())
            {
                serializer.Serialize(writer, pair.Value);
            }

            writer.WriteEndObject();
        }
    }
}
