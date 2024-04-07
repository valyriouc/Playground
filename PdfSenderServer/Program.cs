
using System.Net.Sockets;
using System.Reflection;

internal static class Server
{
    public static void Main()
    {
        using TcpListener listener = new TcpListener(8888);

        listener.Start();

        Console.WriteLine("Waiting for the output!");
        using var client = listener.AcceptTcpClient();
        Console.WriteLine("Got you");

        using NetworkStream streaming = client.GetStream();

        using FileStream stream = File.OpenWrite(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "fuckyou.pdf"));

        streaming.CopyTo(stream);
    }
}