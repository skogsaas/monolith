using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace RFBridge
{
    public class Gateway
    {
        private UdpClient udp;

        public Gateway()
        {
            this.udp = new UdpClient();
            this.udp.Connect(IPAddress.Parse("10.0.0.10"), 5000);
        }

        #region NEXA

        public async void nexaDeviceOnOff(uint group, byte device, bool on)
        {
            byte[] data = new byte[7];
            data[0] = 1;

            Array.Copy(toNetworkOrder(BitConverter.GetBytes(group)), 0, data, 1, 4);

            data[5] = device;
            data[6] = (byte)(on ? 1 : 0);

            await this.udp.SendAsync(data, data.Length);
        }
        public async void nexaDeviceDim(uint group, byte device, byte dim)
        {
            byte[] data = new byte[7];
            data[0] = 2;

            Array.Copy(toNetworkOrder(BitConverter.GetBytes(group)), 0, data, 1, 4);

            data[5] = device;
            data[6] = dim;

            await this.udp.SendAsync(data, data.Length);
        }

        public async void nexaGroupOnOff(uint group, bool on)
        {
            byte[] data = new byte[6];
            data[0] = 3;

            Array.Copy(toNetworkOrder(BitConverter.GetBytes(group)), 0, data, 1, 4);

            data[5] = (byte)(on ? 1 : 0);

            await this.udp.SendAsync(data, data.Length);
        }

        #endregion

        #region Utils

        private byte[] toNetworkOrder(byte[] data)
        {
            if(BitConverter.IsLittleEndian)
            {
                Array.Reverse(data);
            }

            return data;
        }

        #endregion
    }
}
