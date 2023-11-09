
namespace Parentheses;

public class Solution
{
    private int Offset { get; set; } = -1;
    private List<char> Stack { get; set; } = new List<char>();

    private char Pop()
    {
        char s = Stack[Offset];
        Offset -= 1;
        Stack.Remove(s);
        return s;
    }

    private void Push(char sign)
    {
        Offset += 1;
        Stack.Add(sign);
    }

    public bool IsValid(string s)
    {
        if (s.Length % 2 != 0)
        {
            return false;
        }

        int counter = 0;
        foreach (char c in s)
        {
            if (c == '(' || c == '[' || c == '{')
            {
                counter += 1;
                Push(c);
                continue;
            }
            char res = Pop();
            if (c == ')' && res != '(' ||
                c == ']' && res != '[' ||
                c == '}' && res != '{')
            {
                return false;
            }
            else if (c == ')' && res == '(' ||
                c == ']' && res == '[' ||
                c == '}' && res == '{')
            {
                counter -= 1;
            }
        }

        return counter == 0;
    }
}