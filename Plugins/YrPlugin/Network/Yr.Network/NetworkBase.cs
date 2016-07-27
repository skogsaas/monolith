using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Xml.Xsl;

namespace Yr.Network
{
    public abstract class NetworkBase<T> : NetworkRequest, INetworkBase
    {
        JsonSerializer serializer = new JsonSerializer();

        public T createObjectFromJson(string value)
        {
            return (T)JsonConvert.DeserializeObject<T>(value);
        }

        public T createObjectFromXml(string value)
        {
            StringReader sr = new StringReader(value);
            
            XmlSerializer serializer = new XmlSerializer(typeof(T));

            return (T)serializer.Deserialize(sr);       
        }
    }
}
