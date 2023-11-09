using System.Linq.Expressions;

namespace TestingDatabaseAccess
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Expression<Func<int>> expr = () => 2 + 2;
        }
    }
}