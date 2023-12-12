using System.Net;
using System.Reflection.Metadata;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace Pipelining;

internal interface ICsvWriteable {

    public void WriteCsv(TextWriter writer);
}

internal enum Gender {
    Male = 0,
    Female = 1
}

internal struct User : ICsvWriteable {
    public int Id {get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public Gender Gender { get; set; }

    public IPAddress IpAddress {get; set; }

    public User() {

    }

    public void WriteCsv(TextWriter writer)
    {
        writer.Write($"{Id},");
        writer.Write($"{FirstName},");
        writer.Write($"{LastName},");
        writer.Write($"{Email},");
        writer.Write($"{Gender.ToString()},");
        writer.Write($"{IpAddress}");
        writer.WriteLine();
    }
}

internal class CsvConverter<T> 
    where T : ICsvWriteable {

    public IEnumerable<Task<List<T>>> Payload { get; }

    public CsvConverter(IEnumerable<Task<List<T>>> payload) {
        Payload = payload;
    }

    public async Task WritePayloadAsync() {
        
        using FileStream stream = File.OpenWrite("output.csv");

        using StreamWriter writer = new StreamWriter(stream);

        IEnumerator<Task<List<T>>> enumerator = Payload.GetEnumerator();

        while(enumerator.MoveNext()) {
            try {
                IEnumerable<T> payload = await enumerator.Current;
                foreach (T test in payload) {
                    test.WriteCsv(writer);
                }
            }
            catch(Exception ex) {
                Console.WriteLine(ex.Message);
            }
        } 

        await writer.FlushAsync();
    }
}

class Program
{   
    static async Task Main(string[] args)
    {
        // IEnumerable<IEnumerable<Task<User>>> 
        IEnumerable<CsvConverter<User>> result = Directory.EnumerateDirectories("test")
            .Select(GetFilePaths)
            .Select(LoadFileContent)
            .Select(TransformTo)
            .Select(x => new CsvConverter<User>(x));

        foreach (CsvConverter<User> conv in result) {
            await conv.WritePayloadAsync();
        }
            
    }

    static IEnumerable<string> GetFilePaths(string dirPath) {
        return Directory.EnumerateFiles(dirPath);
    }

    static IEnumerable<Task<string>> LoadFileContent(IEnumerable<string> filepaths) {
        foreach (string filepath in filepaths) {
            Console.WriteLine(filepath);
            yield return File.ReadAllTextAsync(filepath);
        }
    }

    static IEnumerable<Task<List<User>?>> TransformTo(IEnumerable<Task<string>> contents) {
        foreach (Task<string> userTask in contents) {
            yield return userTask.ContinueWith(testing => JsonSerializer.Deserialize<List<User>>(testing.Result));
        }
    }
}
