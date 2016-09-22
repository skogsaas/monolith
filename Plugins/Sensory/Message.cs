namespace Monolith.Plugins.Sensory
{
    public class Message
    {
        public byte SensorId { get; set; }
    }

    public class DeviceMessage : Message
    {
        public enum MessageTypes : byte
        {
            Pull = 1,
            Push = 2
        }

        public MessageTypes Type { get; set; }
        public byte DataId { get; set; }
        public byte[] Data { get; set; }
    }

    public class DeviceDataMessage<T> : DeviceMessage
    {
        public byte Size { get; set; }
        public T Data { get; set; }
    }
}