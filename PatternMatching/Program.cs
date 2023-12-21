
using System.Collections;

namespace PatternMatching;

internal static class Program
{
    public static void Main()
    {
        Span<char> testing = new Span<char>("hello world".ToArray());

        GetResultConstantIf(testing);

        string result = GetResultConstantPatternSwitch(testing);

        Console.WriteLine(result);

        string furtherResult = ConstantPatternSwitchInt(200);

        Console.WriteLine(furtherResult);

        if ("Hello world is dead" is string tmp)
        {
            Console.WriteLine(tmp);
        }

        string anotherResult = TypeMatching(new string[] { "hello world" });
        Console.WriteLine(anotherResult);

        string typeResult = TypeMatching(new List<string>() { "hello", "world" });
        Console.WriteLine(typeResult);

        if (new List<string>() { "Tehere", "test" } is IEnumerable vargar)
        {
            foreach (string varg in vargar)
            {
                Console.WriteLine(varg);
            }
        }
    }

    public static void GetResultConstantIf(Span<char> span)
    {
        if (span is "hello world")
        {
            Console.WriteLine("We got a hello world");
        }
        else
        {
            Console.WriteLine("Nice try but no match");
        }
    }

    public static string GetResultConstantPatternSwitch(Span<char> span) => span switch
    {
        "hello world" => "We got a hit",
        "Hello no world" => "We got another hit",
        _ => "No hit at all"
    };

    public static string ConstantPatternSwitchInt(int input) => input switch
    {
        100 => "We are hundert",
        200 => "We are two hundert",
        _ => "We have no number"
    };

    public static string TypeMatching(IEnumerable<string> input) => input switch
    {
        Array _ => "We have an array",
        IEnumerable => "We got an enumerable",
        _ => "We have no check"
    };
}