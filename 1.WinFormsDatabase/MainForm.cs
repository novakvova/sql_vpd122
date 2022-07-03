using System.Data.SqlClient;

namespace _1.WinFormsDatabase
{
    public partial class MainForm : Form
    {
        SqlConnection con;
        string dirSql = "SqlTabels";
        public MainForm()
        {
            InitializeComponent();
        }

        private void btnGenerateTabels_Click(object sender, EventArgs e)
        {
            string dbName = "myDatabase";
            string conSTR = "Data Source=.;Integrated Security=True;";
            string conectionSTR = $"{conSTR}Initial Catalog={dbName}";
            con = new SqlConnection(conectionSTR);
            con.Open();

            CreateTabels();
        }

        private void CreateTabels()
        {
            ExecuteCommandFromFile("tblRegions.sql");
            ExecuteCommandFromFile("tblCities.sql");
            ExecuteCommandFromFile("tblUsers.sql");
            ExecuteCommandFromFile("tblRoles.sql");
            ExecuteCommandFromFile("tblUserRoles.sql");
            ExecuteCommandFromFile("tblUserAdresses.sql");
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