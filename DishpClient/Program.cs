using System.Net.Sockets;
using System.Text;

namespace Dishp.Client;

internal static class Program
{
    public static void Main(string[] args)
    {
        int id = int.Parse(args[0]);
        using TcpClient client = new TcpClient();
       
        client.Connect("127.0.0.1", 4300);

        while(true)
        {
            try
            {
                string payload = $"ASK {id}\n---\nFor:Action\n---\n{{\"Hello\":\"Nice\"}}\n---";

                NetworkStream stream = client.GetStream();

                byte[] res = Encoding.UTF8.GetBytes(payload);
                stream.Write(res, 0, res.Length);
            }
            catch (Exception ex)
            {
                continue;
            }
            finally
            {
                client.Close();
            }
        }

        Console.WriteLine("Connection ended!");
    }
}