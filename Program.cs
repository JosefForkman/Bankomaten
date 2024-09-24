namespace Bankomaten
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 
            * user id
            * name
            * pin 
            */
            string[][] users = [
                [ "1", "Mickey", "1234" ],
                [ "2", "Donald Duck", "1234" ],
                [ "3", "Goofy", "1234" ]
            ];
            string[][] ballances = [
                [ users[0][0], "lönekonto", "1000" ],
                [ users[0][0], "sparkonto", "2000" ],
                [ users[1][0], "lönekonto", "3000" ]
            ];

            Console.WriteLine("Welcome to the Jos banken");
            string[]? user = SignIn(users);

            /* Validates a user exist rest of the code */
            if (user == null)
            {
                return;
            }

            Console.WriteLine($"Welcome {user[1]}");

            user = Menu(user);
        }

        private static string Ask(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine() ?? "";
        }
        /// <summary>
        /// Sign in the user.
        /// </summary>
        /// <param name="users"></param>
        /// <returns>Returns a user if the user uses the correct credentials otherwise null.</returns>
        private static string[]? SignIn(string[][] users)
        {
            string[]? user = null;

            /* If the user is not found givs tow more trays */
            for (int i = 0; i < 3; i++)
            {
                string name = Ask("What is your name?");
                int pin;
                while (!int.TryParse(Ask("What is your pin?"), out pin))
                {
                    Console.WriteLine("Invalid pin, try again");
                }

                /* Finds the user */
                for (int k = 0; k < users.GetLength(0); k++)
                {
                    if (users[k][1] == name && int.Parse(users[k][2]) == pin)
                    {
                        user = users[k];
                    }
                }

                if (user != null)
                {
                    return user;
                }

                Console.WriteLine("Wrong name or pin, try again");
            }

            return user;
        }
        private static string[]? SignOut(string[]? user)
        {
            if (user != null)
            {
                return null;
            }
            return user;
        }

        /// <summary>
        /// This method take care of the menu of this cash machine
        /// </summary>
        /// <param name="user"></param>
        /// <returns>The state of the current user.</returns>
        private static string[]? Menu(string[]? user)
        {
            while (true)
            {
                Console.WriteLine("Click enter to get to the main menu");
                ConsoleKeyInfo key = Console.ReadKey(true);

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.Clear();

                    Console.WriteLine("The menu\n\n");

                    Console.WriteLine("1. See your accounts and balance");
                    Console.WriteLine("2. Transfer between accounts");
                    Console.WriteLine("3. Withdraw money");
                    Console.WriteLine("4. sign out");
                    int option;
                    while (!int.TryParse(Ask("Choose a one of the options by write the number of the option"), out option))
                    {
                        Console.WriteLine("invalid choice");
                    }

                    switch (option)
                    {
                        case 1:
                            Console.WriteLine("See your accounts and balance");
                            break;
                        case 2:
                            Console.WriteLine("Transfer between accounts");
                            break;
                        case 3:
                            Console.WriteLine("3. Withdraw money");
                            break;
                        case 4:
                            Console.WriteLine("4. sign out");
                            Menu(user);
                            break;
                        default:
                            Console.WriteLine("invalid choice");
                            break;
                    }
                }
            }
        }
    }
}