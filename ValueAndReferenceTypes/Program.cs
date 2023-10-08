namespace ValueAndReferenceTypes
{
    internal class Testing
    {
        public string Name { get; set; }

        public int Value { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Testing test = new()
            {
                Name = "test",
                Value = 42
            };

            int aVariable = 2100;
            ReadOnly(aVariable);

            Console.WriteLine(aVariable);

            ReadOnly(in aVariable);
            Console.WriteLine(aVariable);

            ReadOnly(in test);

            Console.WriteLine(test.Name);
            Console.WriteLine(test.Value);
        }

        static void ReadOnly(int value)
        {
            value = 233;
            Console.WriteLine(value);   
        }

        static void ReadOnly(in int value)
        {
            /*
             * value = 234324; // This does not works because the in parameter disables the modification of a variable value (reference/actual value)
             */
            Console.WriteLine(value);
        }

        static void ReadOnly(in Testing testing)
        {
            testing.Name = "Hello"; // The members can be manipulated 
            testing.Value = 4454;

            /*
             * testing = new () // This does not works because the in parameter disables the modification of a variable value (reference/actual value)
             * 
             */

            Console.WriteLine(testing.Name);
            Console.WriteLine(testing.Value);
        }
    }
}