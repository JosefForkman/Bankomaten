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

            user = SignOut(user);
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
                user = users.ToList().Find(user => user[1] == name && int.Parse(user[2]) == pin);

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
            if (user == null)
            {
                return [];
            }
            return user;
        }
    }
}