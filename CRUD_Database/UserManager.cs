using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Database
{
    public class UserManager : IDisposable
    {
        private string strCon = "Data Source=DESKTOP-FE25KKP;Initial Catalog=test;Integrated Security=True";
        private SqlConnection con;
        public UserManager()
        {
            //con = new SqlConnection(strCon);
            //con.Open();
        }
        public List<User> Users
        {
            get
            {
                List<User> users = new List<User>();
                string sql = "Select * from tblUsers";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    var user = new User
                    {
                        Id = int.Parse(dr["Id"].ToString()),
                        Email = dr["Email"].ToString(),
                        FirstName = dr["FirstName"].ToString(),
                        LastName = dr["LastName"].ToString()
                    };
                    users.Add(user);
                }
                return users;
            }
        }


        public void CreateDatabase()
        {
            try
            {
                string strCon = "Data Source=.;Integrated Security=True";
                string strQuery = "CREATE DATABASE new";
                using (con = new SqlConnection(strCon))
                {
                    SqlCommand cmd = new SqlCommand(strQuery, con);
                    cmd.Connection.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch(Exception ex)
            {

            }
            
        }

        public void CreateTableDatabase()
        {
            string strCon = "Data Source = .; Integrated security = true; database = New";
            string strQuery = "CREATE TABLE tblUsers(ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY," +
                        "EMAIL NVARCHAR(50) NOT NULL CHECK(EMAIL <> N'')," +
                        "FIRSTNAME NVARCHAR(50) NOT NULL CHECK(FIRSTNAME <> N'')," +
                        "LASTNAME NVARCHAR(50) NOT NULL CHECK(LASTNAME <> N''))" +
                        "CREATE TABLE tblRoles(ID INT NOT NULL IDENTITY (1,1) PRIMARY KEY," +
                        "NAME NVARCHAR(50) NOT NULL CHECK(NAME <> N''))" +
                        "CREATE TABLE tblUserRoles(ID INT NOT NULL IDENTITY(1,1) PRIMARY KEY," +
                        "USERID INT NOT NULL UNIQUE FOREIGN KEY REFERENCES tblUsers(ID)," +
                        "ROLEID INT NOT NULL UNIQUE FOREIGN KEY REFERENCES tblRoles(ID)) ";
            using (con = new SqlConnection(strCon))
            {
                SqlCommand cmdCT = new SqlCommand(strQuery, con);
                cmdCT.Connection.Open();
                try
                {
                    cmdCT.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public User Create(UserCreate user)
        {
            string sql = $"INSERT INTO tblUsers ([Email],[FirstName],[LastName]) VALUES (N'{user.Email}',N'{user.FirstName}',N'{user.LastName}');" +
                $"SELECT CAST(scope_identity() AS int) ";
            SqlCommand cmd = new SqlCommand(sql, con);
            int id = (int)cmd.ExecuteScalar();
            
            return new User
            {
                Id=id,
                Email=user.Email,
                FirstName=user.FirstName,
                LastName=user.LastName
            };
        }
        
        public void Dispose()
        {
            con.Close();
        }
    }
}
