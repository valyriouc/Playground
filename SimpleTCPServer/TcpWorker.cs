using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SimpleTCPServer
{
    internal class TcpWorker
    {
        public TcpClient Client { get; }

        public NetworkStream Stream => Client.GetStream();

        public bool IsFinished { get; private set; }

        private byte[] Buffer { get; set; } 

        public TcpWorker(TcpClient client)
        {
            Client = client;
        }

        private Span<byte> ResetBuffer()
        {
            Buffer = new byte[256];
            return new Span<byte>(Buffer);
        }

        public void Run()
        {
            Span<byte> buffer = ResetBuffer();

            while (!IsFinished)
            {
                IsFinished = Stream.Read(buffer) <= 0;
                Console.WriteLine(Encoding.UTF8.GetString(buffer));
                buffer = ResetBuffer();
            }

            Client.Close();
        }
    }
}
