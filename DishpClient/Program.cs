
using System.Net.Sockets;
using System.Text;

namespace Dishp.Client;

internal static class Program
{
    public static void Main()
    {
        TcpClient client = new TcpClient();
       
        client.Connect("127.0.0.1", 13000);

        string payload = "ASK\n---\nFor:Action\n---{\"Hello\":\"Nice\"}";

        NetworkStream stream = client.GetStream();

        var res = Encoding.UTF8.GetBytes(payload);
        stream.Write(res, 0, res.Length);

        Console.WriteLine($"Send {res.Length} bytes");

        while(client.Connected)
        {

        }

        client.Close();

        Console.ReadLine();
    }
}