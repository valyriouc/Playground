
internal static class Program
{
    public static void Main()
    {
        EventWaitHandle handle = new EventWaitHandle(false, EventResetMode.AutoReset, "myhandle");

        Console.WriteLine("We are waiting 5 seconds!");
        Thread.Sleep(5000);
        Console.WriteLine("Now everybody gets notified!");
        handle.Set();

        Console.WriteLine("Everything done!");
    }
}