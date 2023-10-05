
using System.Net.Sockets;
using System.Text;

namespace Dishp.Client;

internal static class Program
{
    public static void Main()
    {
        TcpClient client = new TcpClient();
       
        client.Connect("127.0.0.1", 4300);

        string payload = "ASK\n---\nFor:Action\n---\n{\"Hello\":\"Nice\"}\n---";

        NetworkStream stream = client.GetStream();

        byte[] res = Encoding.UTF8.GetBytes(payload);
        stream.Write(res, 0, res.Length);

        Console.WriteLine("Connection closed!");
        client.Close();
    }
}