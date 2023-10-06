using SimpleTCPServer;

using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SimpleTCPClient;

internal static class Program
{
    private static int Port { get; } = 4300;

    public static async Task Main()
    {
        IPAddress address = IPAddress.Parse("127.0.0.1");
        
        TcpListener listener = new TcpListener(address, Port);

        listener.Start();
        Console.WriteLine($"Listening on port {Port}");

        while (true)
        {
            Console.WriteLine("Waiting for connection");

            using TcpClient client = listener.AcceptTcpClient();

            try
            { 
                Thread worker = new Thread(new ThreadStart(() => new TcpWorker(client).Run()));

                worker.Start();

                Console.WriteLine("Connection was ended!");
            }
            catch (Exception ex)
            {
             
            }
            finally
            {
                client.Close();
            }
            
        }
    }
}
