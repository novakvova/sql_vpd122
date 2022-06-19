using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Database
{
    public class DatabaseManager
    {
        private readonly SqlConnection con;
        public DatabaseManager(string connStr)
        {
            con = new SqlConnection(connStr);
            con.Open();
        }

        public void CreateDB(string name)
        {
            string sql = $"CREATE DATABASE [{name}];";
            SqlCommand command = con.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery(); //посилаємо команду в БД
        }

        public void DeleteDB(string name)
        {
            string sql = $"DROP DATABASE [{name}];";
            SqlCommand command = con.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery(); //посилаємо команду в БД
        }

        public void ShowAllDatabase()
        {
            string sql = $"SELECT name FROM master.sys.databases";
            using (SqlCommand command = con.CreateCommand())
            {
                command.CommandText = sql;
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine(reader["name"]);
                    }
                }
            }
        }
    }
}
