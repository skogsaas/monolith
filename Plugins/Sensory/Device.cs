using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Monolith.Plugins.Sensory
{
    public class Device : Devices.DeviceBase
    {
        private Gateway gateway;
        private SensoryConfiguration config;

        public Framework.Channel Channel;

        public string Identifier { get; private set; }

        public IPAddress Address { get; private set; }
        public ushort Port { get; private set; }

        public List<Sensor> Sensors { get; private set; }

        public Device(Gateway g, Framework.Channel s, SensoryConfiguration c)
            : base(typeof(Device).FullName + "." + c.Identifier)
        {
            this.gateway = g;
            this.Channel = s;
            this.config = c;

            this.Identifier = c.Identifier;
            this.Address = IPAddress.Parse(c.IpAddress);
            this.Port = c.Port;
            this.Sensors = new List<Sensor>();

            foreach(KeyValuePair<string, Framework.String> pair in this.config.Sensors)
            {
                int sensorId = int.Parse(pair.Key);
                
                if(pair.Value == typeof(DhtSensor).Name)
                {
                    this.Sensors.Add(new DhtSensor(this, (byte)sensorId));
                }
                else if (pair.Value == typeof(DigitalSensor).Name)
                {
                    this.Sensors.Add(new DigitalSensor(this, (byte)sensorId));
                }
                else
                {
                    Logging.Logger.Error("Unable to initialize device " + this.Identifier + " sensor " + sensorId + " of type " + pair.Value);
                }
            }
        }

        public void Handle(byte[] data)
        {
            DeviceMessage msg = new DeviceMessage();
            msg.SensorId = data[0];
            msg.Type = (DeviceMessage.MessageTypes)data[1];
            msg.DataId = data[2];

            byte length = data[3];

            msg.Data = new byte[length];
            Array.Copy(data, 4, msg.Data, 0, msg.Data.Length);

            handle(msg);
        }

        private void handle(DeviceMessage msg)
        {
            foreach(Sensor sensor in this.Sensors)
            {
                if(sensor.Id == msg.SensorId)
                {
                    sensor.handle(msg);

                    break;
                }
            }
        }
    }
}
