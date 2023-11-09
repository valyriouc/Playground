using MySqlConnector;

using System.Data;

namespace AdoNetPrototyping
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MySqlConnection conn = new MySqlConnection("server=localhost;database=adotesting;uid=vector;pwd=K/]zjUT)({?Xbdy?<+YEpsNzB38,*0$rc7DiAqvL");

            conn.Open();

            MySqlCommand cmd = conn.CreateCommand();

            cmd.CommandText = "SELECT * FROM users";

            MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);

            DataSet set = new DataSet();

            adapter.Fill(set);

            var reader = set.CreateDataReader();

            while(reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    Console.Write(reader[i].ToString());
                    Console.Write(" ");
                }
            }

            conn.Close();
        }
    }
}