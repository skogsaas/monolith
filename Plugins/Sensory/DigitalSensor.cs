using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monolith.Plugins.Sensory
{
    public class DigitalSensor : Sensor
    {
        private Device device;

        private enum DataIds : byte
        {
            Pin = 1
        }

        public Signaling.Signal<bool> Pin { get; private set; }

        public DigitalSensor(Device d, byte s)
        {
            this.device = d;
            this.Id = s;

            this.Pin = new Signaling.Signal<bool>(this.device.Identifier + "." + this.Type + "." + this.Id + ".Pin");

            this.Signals.Add(this.Pin);

            this.device.Channel.publish(this.Pin);
        }

        public override void handle(DeviceMessage msg)
        {
            if(msg.Type == DeviceMessage.MessageTypes.Push)
            {
                if (msg.DataId == (byte)DataIds.Pin)
                {
                    try
                    {
                        this.Pin.State.Value = msg.Data[0] != 0;
                    }
                    catch(Exception ex)
                    {
                    }
                }
                else
                {
                    Logging.Logger.Warning("Unable to handle push for data id " + msg.DataId + " for sensor type DigitalSensor " + this.device.Identifier);
                }
            }
        }
    }
}
