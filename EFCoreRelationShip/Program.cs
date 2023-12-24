//using Microsoft.EntityFrameworkCore;

//using System.Text.Json;
//using System.Text.Json.Serialization;

//namespace EFCoreRelationShip
//{
//    internal class Program
//    {
//        static void Main(string[] args)
//        {
//            using TestingDbContext dbContext = new TestingDbContext();

//            Microsoft.EntityFrameworkCore.Metadata.IModel model = dbContext.Model;

//            foreach (User user in dbContext.Users.Include(x => x.Notes))
//            {
//                string json = JsonSerializer.Serialize(user, new JsonSerializerOptions()
//                {
//                    ReferenceHandler = ReferenceHandler.IgnoreCycles
//                }

//                Console.WriteLine(json);
//            }
//        }
//    }
//}