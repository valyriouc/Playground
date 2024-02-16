
internal static class Program
{
    public static void Main(string[] args)
    {
        EventWaitHandle myHandle = new EventWaitHandle(false, EventResetMode.AutoReset, "myhandle");

        Console.WriteLine("We are now waiting for a signal...!");
        myHandle.WaitOne();
        Console.WriteLine("We are finished with waiting!");
        Console.WriteLine("Shutting down!");
    }
}