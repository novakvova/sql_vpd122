using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Database
{
    public class TableManager
    {
        private readonly SqlConnection con;
        private string dirSql = "SqlQuery";

        public TableManager(string connStr)
        {
            con = new SqlConnection(connStr);
            con.Open();
        }

        public void CreateTabels()
        {
            //Користувачі
            ExecuteCommandFromFile("tblUsers.sql");
        }

        private void ExecuteCommandFromFile(string file)
        {
            string sql = ReadSqlFile(file);
            SqlCommand command = con.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery(); //посилаємо команду в БД
        }
        private string ReadSqlFile(string file)
        {
            string sql = File.ReadAllText($"{dirSql}\\{file}");
            return sql;
        }

    }
}
