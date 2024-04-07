
using System.Net;
using System.Net.Sockets;
using System.Reflection;

internal static class App
{
    public static void Main()
    {
        using TcpClient client = new TcpClient();
        
        IPEndPoint endpoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
        client.Connect(endpoint);

        using NetworkStream stream = client.GetStream();

        using FileStream inputStream = File.OpenRead(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "pruefung.pdf"));

        inputStream.CopyTo(stream);
    
    }
}