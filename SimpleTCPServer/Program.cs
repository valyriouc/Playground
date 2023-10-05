
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimpleTCPClient;

internal static class Program
{
    private static int Port { get; } = 4300;

    public static void Main()
    {
        IPAddress address = IPAddress.Parse("127.0.0.1");
        
        TcpListener listener = new TcpListener(address, Port);

        listener.Start();
        Console.WriteLine($"Listening on port {Port}");

        while (true)
        {
            using TcpClient client = listener.AcceptTcpClient();

            Console.WriteLine("Connection received!");

            NetworkStream stream = client.GetStream();

            bool isFinished = false;
            Span<byte> buffer = new Span<byte>(new byte[20]);

            while (!isFinished)
            {
                isFinished = stream.Read(buffer) <= 0;
                Console.WriteLine(Encoding.UTF8.GetString(buffer));
                Console.WriteLine($"Received {buffer.ToArray().Where(b => b != 0).Count()}");
                buffer = new Span<byte>(new byte[128]);
            }

            Console.WriteLine("Connection terminated!");
            client.Close();
        }
    }
}
