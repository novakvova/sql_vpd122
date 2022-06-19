
using Microsoft.Extensions.Configuration;
using System.Text;

namespace CRUD_Database
{
    public class Program
    {
        public static void Main(String[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            string connectionDb = configuration.GetConnectionString("BloggingDatabase");
            int action = 0;

            do
            {
                Console.WriteLine("0. Вихід");
                Console.WriteLine("1. Керування базами даних");
                Console.WriteLine("2. Керування окремою БД");
                action = int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        {
                            WorkDatabases(connectionDb);
                            break;
                        }
                    case 2:
                        {
                            Console.WriteLine("Введіть назву БД:");
                            string dbName = Console.ReadLine();
                            WorkTabelsInDB(connectionDb, dbName);
                            break;
                        }

                }
            } while (action != 0);
        }

        
        //Керування Базами даних
        static void WorkDatabases(string conSTR)
        {
            DatabaseManager dbManager = new DatabaseManager(conSTR);
            int action = 0;
            do
            {
                Console.WriteLine("0. Вихід");
                Console.WriteLine("1. Створити БД");
                Console.WriteLine("2. Видалити БД");
                Console.WriteLine("3. Показати список БД");
                action = int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        {
                            try
                            {
                                string name;
                                Console.WriteLine("Введіть назву БД:");
                                name = Console.ReadLine();
                                dbManager.CreateDB(name);
                                Console.WriteLine("------Базу даних успішно створено----");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Помилка створення БД --", ex.Message);
                            }
                            break;
                        }
                    case 2:
                        {
                            string name;
                            Console.WriteLine("Введіть назву БД:");
                            name = Console.ReadLine();
                            dbManager.DeleteDB(name);
                            Console.WriteLine("------Базу даних успішно видалено----");
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Список БД:");
                            dbManager.ShowAllDatabase();
                            break;
                        }
                }
            } while (action != 0);
        }

        static void WorkTabelsInDB(string conSTR, string dbName)
        {
            string conectionSTR = $"{conSTR}Initial Catalog={dbName}";
            TableManager tableManager = new TableManager(conectionSTR);
            int action = 0;
            do
            {
                Console.WriteLine("0. Вихід");
                Console.WriteLine("1. Cтворити таблиці");
                Console.WriteLine("2. Заповнити БД по замовчюванню");
                Console.WriteLine("3. Заповнити даними BogusRandom");
                Console.WriteLine("4. Показати продукти");
                Console.WriteLine("5. Заповнити даними за допомогою SP");
                Console.WriteLine("6. Провести операцію в транзації");
                action = int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        {
                            tableManager.CreateTabels();
                            break;
                        }
                    case 2:
                        {
                            
                            break;
                        }
                    case 3:
                        {
                           
                            break;
                        }
                    case 4:
                        {
                           
                            break;
                        }
                    case 5:
                        {
                            
                            break;
                        }
                    case 6:
                        {
                            
                            break;
                        }
                }
            } while (action != 0);
        }


    }
}


