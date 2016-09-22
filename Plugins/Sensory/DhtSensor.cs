using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Plugins.Sensory
{
    public class DhtSensor : Sensor
    {
        private Device device;

        private enum DataIds : byte
        {
            Temperature = 1,
            Humidity = 2
        }

        public Signaling.Signal<double> Temperature { get; private set; }
        public Signaling.Signal<double> Humidity { get; private set; }

        public DhtSensor(Device d, byte s)
        {
            this.device = d;
            this.Id = s;

            this.Temperature = new Signaling.Signal<double>(this.device.Identifier + "." + this.Type + "." + this.Id + ".Temperature");
            this.Humidity = new Signaling.Signal<double>(this.device.Identifier + "." + this.Type + "." + this.Id + ".Humidity");

            this.Signals.Add(this.Temperature);
            this.Signals.Add(this.Humidity);

            this.device.Channel.publish(this.Temperature);
            this.device.Channel.publish(this.Humidity);
        }

        public override void handle(DeviceMessage msg)
        {
            if(msg.Type == DeviceMessage.MessageTypes.Push)
            {
                if (msg.DataId == (byte)DataIds.Temperature)
                {
                    try
                    {
                        int d = BitConverter.ToInt32(msg.Data, 0);
                        this.Temperature.State.Value = ((double)d) / 1000;
                    }
                    catch(Exception ex)
                    {
                    }
                }
                else if (msg.DataId == (byte)DataIds.Humidity)
                {
                    try
                    {
                        int d = BitConverter.ToInt32(msg.Data, 0);
                        this.Humidity.State.Value = ((double)d) / 1000;
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    Logging.Logger.Warning("Unable to handle push for data id " + msg.DataId + " for sensor type DhtSensor " + this.device.Identifier);
                }
            }
        }
    }
}
