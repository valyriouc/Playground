
using System.Net;
using System.Net.Sockets;

namespace SimpleTCPClient;

internal static class Program
{
    public static void Main()
    {
        IPAddress address = IPAddress.Parse("127.0.0.1");

        TcpListener listener = new TcpListener(address, 4454);

        listener.Start();
        Console.WriteLine("Listening...");

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Connection received");

            using Stream stream = client.GetStream();

            using StreamReader reader = new StreamReader(stream);

            Console.WriteLine("<<<<<< Content >>>>>");
            Console.WriteLine(reader.ReadToEnd());
        }
    }
}
