using System.Text;
using System.Text.Json;

namespace WorkingWithBytesAndStreams
{
    enum MethodKind
    {
        ASK = 0,
        ANS = 1,
    }

    enum MethodFunction
    {
        Action = 0,
        Transfer = 1
    }

    class Request
    {
        public MethodKind Kind { get; set; }

        public MethodFunction Function { get; set; }

        public byte[] Content { get; set; }

        public TEntity ReadFromBody<TEntity>()
        {
            if (Content == null)
            {
                throw new NullReferenceException(nameof(Content));
            }

            TEntity? entity = JsonSerializer.Deserialize<TEntity>(Content!);
            
            if (entity == null)
            {
                throw new Exception("Could not parse Entity"); 
            }

            return entity;
        }
    }

    internal class Program
    {
        public static Request Request { get; } = new Request();

        static void Main(string[] args)
        {
            MemoryStream stream = new MemoryStream();

            byte[] buffer = Encoding.UTF8.GetBytes(
                "ASK\n---\nFor: Action\n---\n{\"hello\":\"Nice try\"}\n---");

            stream.Write(buffer, 0, buffer.Length);

            stream.Position = 0;
            ParseMethodKind(stream);

            Console.WriteLine(Request.Kind);
        }

        static void ParseMethodKind(MemoryStream stream)
        {
            byte[] buffer = new byte[6];
            int offset = stream.Read(buffer, 0, buffer.Length);

            string methodKind = string.Empty;
            for (int i = 0; i < 3; i++)
            {
                methodKind += (char)buffer[i];
            }

            for (int i = 4; i < 6; i++)
            {
                ThrowIfNoDivider(buffer[i], "Expected method divider");
            }

            Request.Kind = MapMethodType(methodKind);

            ParseHeaders(stream, 0 + offset);
        }

        static void ThrowIfNoDivider(byte actual, string onErrorMessage)
        {
            if (actual != (byte)'-')
            {
                throw new Exception(onErrorMessage);
            }
        }

        static MethodKind MapMethodType(string methodType) => methodType switch
        {
            "ASK" => MethodKind.ASK,
            "ANS" => MethodKind.ANS,
            _ => throw new Exception("Method not supported")
        }; 

        static void ParseHeaders(MemoryStream stream, int offset)
        {
           
        }

        static MethodFunction MapMethodFunction(string methodFunction) => methodFunction switch
        {
            "action" => MethodFunction.Action,
            "transfer" => MethodFunction.Transfer,
            _ => throw new Exception("Method function is not supported!")
        };
    }
}