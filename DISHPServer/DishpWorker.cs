using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DISHPServer
{
    internal enum DisphMode
    {
        ASK = 0,
        ANS = 1
    }

    internal enum Method
    {
        Action = 0,
        Transfer = 1
    }

    internal enum State
    {
        Init = 0,
        Info = 1
    }

    internal enum Code
    {
        Success = 100,
        Failure = 200
    }

    internal class DishpContext
    {
        public DisphMode Mode { get; set; }

        public Method For { get; set; }

        public Guid Iden { get; set; }

        public State State { get; set; }

        public Code? Code { get; set; }

        public byte[] ByteContent { get; set; }

        public TEntity ContentToJson<TEntity>(TEntity entity)
        {
            TEntity? content = JsonSerializer.Deserialize<TEntity>(ByteContent);
            if (content is null)
            {
                throw new Exception("Could not parse body to entity");
            }
            return content;
        }
    }

    internal enum ParserState
    {
        Reading = 0,
        Parsing = 1,
        Empty = 2
    }

    internal class DishpWorker : IDisposable
    {
        private TcpClient Client { get; init;  }
        
        private int Offset { get; set; }
        
        private bool Done { get; set; }

        public ParserState State { get; set; }

        public Action OnFinished { get; set; }

        public DishpWorker(TcpClient client)
        {
            Client = client;
            Offset = 0;
            Done = false;
            State = ParserState.Reading;
        }

        private Span<byte> Reading()
        {
            Span<byte> bytes = new Span<byte>();

            State = ParserState.Reading;

            NetworkStream stream = Client.GetStream();

            while (stream.Read(bytes) != 0)
            {

            }

            return bytes;
        }

        public void Parse()
        {
            var buffer = Reading();
            Console.WriteLine(buffer.Length);
            foreach (byte b in buffer)
            {
                Console.WriteLine(b);
            }
        }

        private void ParseInternal()
        {

        }

        private bool isDisposed = false;

        private void Dispose(bool disposing)
        {
            if (isDisposed || !disposing)
            {
                return;
            }

            Client.Dispose();
            GC.SuppressFinalize(this);
            isDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
        }
    }
}
