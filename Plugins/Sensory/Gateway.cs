using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Monolith.Plugins.Sensory
{
    public class Gateway
    {
        private UdpClient udp;

        private CancellationTokenSource token;
        private Task task;

        public delegate void PacketEvent(byte[] data, IPAddress addr, ushort port);
        public event PacketEvent Packets;

        public Gateway()
        {
            this.udp = new UdpClient(5001);
            this.token = new CancellationTokenSource();

            this.task = Task.Factory.StartNew(read);
        }

        ~Gateway()
        {
            this.token.Cancel();
        }

        public async Task<bool> send(byte[] data, IPAddress addr, ushort port)
        {
            int bytes = await this.udp.SendAsync(data, data.Length, new IPEndPoint(addr, port));

            return bytes == data.Length;
        }

        private async void read()
        {
            while (!this.token.IsCancellationRequested)
            {
                UdpReceiveResult result = await this.udp.ReceiveAsync();

                if (result != null)
                {
                    this.Packets?.Invoke(result.Buffer, result.RemoteEndPoint.Address, (ushort)result.RemoteEndPoint.Port);
                }
            }
        }
    }
}
