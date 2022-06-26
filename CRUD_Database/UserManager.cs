using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Database
{
    public class UserManager : IDisposable
    {
        private SqlConnection con;
        public UserManager(string strCon)
        {
            con = new SqlConnection(strCon);
            con.Open();
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

        public List<User> SearchUsers(SearchUser searchUser, out int count, int page=1)
        {
            count = 10;
            List<User> users = new List<User>();
            var query = "";
            string sql = "Select * from tblUsers";
            bool isWhere = false;
            if(!string.IsNullOrEmpty(searchUser.FirstName))
            {
                query += $" Where FirstName Like '%{searchUser.FirstName}%'";
                isWhere = true;
            }
            if(!string.IsNullOrEmpty(searchUser.Email))
            {
                if(isWhere)
                    query += $"  AND Email Like '%{searchUser.Email}%'";
                else
                    query += $"  Where Email Like '%{searchUser.Email}%'";
                isWhere = true;
            }
            //кількість запитсів, по запиту
            string queryCount = "Select count(*) as count From tblUsers"+ query;
            using (SqlCommand cmd = new SqlCommand(queryCount, con))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        count = int.Parse(dr["count"].ToString());
                    }
                }
            }
            int rows = (page - 1) * 20;
            sql = sql + query + $" Order by Id  offset {rows} rows  fetch next 20 rows only; ";

            using (SqlCommand cmd = new SqlCommand(sql, con))
            {
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
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
                }
            }

            return users;
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

        public void CreateListUsers(List<UserCreate> users)
        {
            DataTable dt = new DataTable();
            dt.TableName = "tblUsers";
            dt.Columns.Add(new DataColumn("Id"));
            dt.Columns.Add(new DataColumn(nameof(UserCreate.Email)));
            dt.Columns.Add(new DataColumn(nameof(UserCreate.FirstName)));
            dt.Columns.Add(new DataColumn(nameof(UserCreate.LastName)));
            foreach (var user in users)
            {
                DataRow row = dt.NewRow();
                row["Id"] = 0;
                row[nameof(UserCreate.Email)]= user.Email;
                row[nameof(UserCreate.FirstName)] = user.FirstName;
                row[nameof(UserCreate.LastName)]  = user.LastName;
                dt.Rows.Add(row);
            }

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
            {
                bulkCopy.DestinationTableName = "tblUsers";
                bulkCopy.WriteToServer(dt);
            }
        }

        public void Dispose()
        {
            con.Close();
        }
    }
}
