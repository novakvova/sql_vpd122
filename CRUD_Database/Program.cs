using System;
using System.Diagnostics;
using System.Text;
using Bogus;
using Microsoft.Extensions.Configuration;

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
            string conectionSTR = $"{conSTR}Initial Catalog={dbName}; Connect Timeout=120;";
            TableManager tableManager = new TableManager(conectionSTR);
            UserManager userManager = new UserManager(conectionSTR);
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
                Console.WriteLine("7. Пошук даних");
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
                            var faker = new Faker<UserCreate>("uk")
                                .RuleFor(x=>x.FirstName, f=>f.Person.FirstName)
                                .RuleFor(x=>x.LastName, f=>f.Person.LastName)
                                .RuleFor(x=>x.Email, f=>f.Internet.Email());
                            
                            Console.WriteLine("Кількість даних для додавання:");
                            int count = int.Parse(Console.ReadLine());
                            var list = new List<UserCreate>();
                            for (int i = 0; i < count; i++)
                            {
                                var user = faker.Generate();
                                list.Add(user);
                            }
                            Stopwatch stopWatch = new Stopwatch();
                            stopWatch.Start();
                            userManager.CreateListUsers(list);
                            stopWatch.Stop();
                            // Get the elapsed time as a TimeSpan value.
                            TimeSpan ts = stopWatch.Elapsed;

                            // Format and display the TimeSpan value.
                            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                ts.Hours, ts.Minutes, ts.Seconds,
                                ts.Milliseconds / 10);
                            Console.WriteLine("RunTime " + elapsedTime);

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

                    case 7:
                        {
                            Stopwatch stopWatch = new Stopwatch();
                            stopWatch.Start();
                            SearchUser searchUser = new SearchUser();
                            Console.Write("Вкажіть ім'я користувача: ");
                            searchUser.FirstName = Console.ReadLine();
                            Console.Write("Вкажіть пошту: ");
                            searchUser.Email = Console.ReadLine();
                            Console.WriteLine("Enter page number: \n");
                            int page = int.Parse(Console.ReadLine());
                            int count;
                            var users = userManager.SearchUsers(searchUser, out count, page);
                            Console.WriteLine("Read data list: {0}", count);
                            foreach (var user in users)
                            {
                                Console.WriteLine(user);
                            }
                            stopWatch.Stop();
                            // Get the elapsed time as a TimeSpan value.
                            TimeSpan ts = stopWatch.Elapsed;

                            // Format and display the TimeSpan value.
                            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                                ts.Hours, ts.Minutes, ts.Seconds,
                                ts.Milliseconds / 10);
                            Console.WriteLine("RunTime " + elapsedTime);
                            break;
                        }
                }
            } while (action != 0);
        }


    }
}


