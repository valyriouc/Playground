using System.Runtime.InteropServices;

namespace EnumerableTesting
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string field =
                """
                .....
                .$A..
                .A...
                CB..A
                S....
                """;

            char[,] input = new char[5, 5];
            field = field.Replace("\r", "");

            string[] strings = field.Split('\n');

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    input[i, j] = strings[i][j];
                }
            }

            IEnumerable<char> loop = BuildLoop(input, 1, 1);

            foreach (char c in loop)
            {
                Console.Write(c);
            }
        }

        static IEnumerable<char> BuildLoop(char[,] input, int yStart, int xStart)
        {
            List<char> output = new List<char>();
            (bool _, char character) = BuildLoopInternal(input, output, yStart, xStart, yStart, xStart);
            output.Add(character);
            return output;
        }
        
        // TODO: We not ignore the field we came from 

        static (bool,char) BuildLoopInternal(char[,] input, List<char> output, int yStart, int xStart, int yOld, int xOld)
        {
            List<(int, int)> next = new List<(int, int)> ();
            if (yStart > 0)
            {
                next.Add((yStart - 1, xStart));
            }

            if (yStart < 4)
            {
                next.Add((yStart + 1, xStart));
            }

            if (xStart > 0)
            {
                next.Add((yStart, xStart - 1));
            }

            if (xStart < 4)
            {
                next.Add((yStart, xStart + 1));
            }

            bool saveState = false;
            char saveSign = '.';
            foreach ((int y, int x) in next)
            {
                if (input[y,x] == '.' || (y == yOld && x == xOld))
                {
                    continue;
                }

                if (input[y,x] == 'S')
                {
                    return (true, 'S');
                }

                if (input[y,x] == 'A' || input[y,x] == 'B' || input[y,x] == 'C')
                {

                    (bool state, char sign) = BuildLoopInternal(input, output, y, x, yStart, xStart);
                    if (state)
                    {
                        output.Add(sign);
                        saveState = true;
                        saveSign = input[y, x];
                    }
                }
            }

            return (saveState, saveSign);
        }

    }
}
