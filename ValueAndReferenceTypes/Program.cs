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
            // PlayingWithInParameters()

            //PlayingWithRefAssignments();

            PlayingWithValueAssignments();
        }

        static void PlayingWithValueAssignments()
        {
            int value = 23;
            int value2 = value;

            Console.WriteLine(value);
            Console.WriteLine(value2);

            value2 = 4344;

            Console.WriteLine(value);
            Console.WriteLine(value2);
        }

        static void PlayingWithRefAssignments()
        {
            Testing test1 = new()
            {
                Name = "Hello",
                Value = 4343
            };

            Testing test2 = test1;

            Console.WriteLine(test1.Name);
            Console.WriteLine(test2.Name);

            test2.Name = "World";

            Console.WriteLine(test1.Name);
            Console.WriteLine(test2.Name);
        }

        static void PlayingWithInParameters()
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