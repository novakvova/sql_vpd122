
using System.Text;

namespace CRUD_Database
{
    public class Program
    {

        public static void Main(String[] args)
        {
            Console.InputEncoding = Encoding.Unicode;
            Console.OutputEncoding = Encoding.Unicode;

            UserManager userManager = new UserManager();
            int action=0;

            do
            {
                Console.WriteLine("0.Exit");
                Console.WriteLine("1.List Users");
                Console.WriteLine("2.Insert User");
                Console.Write("->_");
                action=int.Parse(Console.ReadLine());
                switch (action)
                {
                    case 1:
                        {
                            var users = userManager.Users;
                            foreach (var item in users)
                            {
                                Console.WriteLine(item);
                            }
                            break;
                        }
                    case 2:
                        {
                            var insetUser = new UserCreate();
                            Console.Write("Enter email: "); insetUser.Email = Console.ReadLine();
                            Console.Write("Enter firstName: "); insetUser.FirstName = Console.ReadLine();
                            Console.Write("Enter lastName: "); insetUser.LastName = Console.ReadLine();
                            var user = userManager.Create(insetUser);
                            //Console.WriteLine(user);
                            break;
                        }
                }

            } while (action!=0);
        }
    }
}

