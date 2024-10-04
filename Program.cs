using System.Globalization;

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
            string[][] users =
            [
                ["1", "Mickey", "1234"],
                ["2", "Donald Duck", "1234"],
                ["3", "Goofy", "1234"],
                ["4", "Pluto", "1234"],
                ["5", "Scar", "1234"]
            ];
            /*
             * user id
             * ballances type
             * amount
             * valuta
             */
            string[][] ballances =
            [
                [users[0][0], "lönekonto", "3456,78", "kr"],
                [users[0][0], "sparkonto", "8912,34", "kr"],
                [users[1][0], "lönekonto", "1234,56", "kr"],
                [users[1][0], "sparkonto", "7890,12", "kr"],
                [users[2][0], "sparkonto", "9876,43", "kr"],
                [users[3][0], "lönekonto", "6543,21", "kr"],
                [users[3][0], "sparkonto", "2109,87", "kr"],
                [users[4][0], "lönekonto", "4321,09", "kr"],
                [users[4][0], "sparkonto", "8765,43", "kr"]
            ];
            bool menuNeedEnter = false;

            Console.WriteLine("Welcome to the Jos banken");
            string[]? user = SignIn(users);

            /* Validates a user exist rest of the code */
            if (user == null)
            {
                return;
            }

            Console.WriteLine($"Welcome {user[1]}");

            while (true)
            {
                ConsoleKeyInfo? currentPreckey = null;
                if (menuNeedEnter)
                {
                    Console.WriteLine("Click enter to get to the main menu");
                    currentPreckey = Console.ReadKey(true);
                }

                if (!menuNeedEnter || currentPreckey?.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine("The menu\n\n");

                    Console.WriteLine("1. See your accounts and balance");
                    Console.WriteLine("2. Transfer between accounts");
                    Console.WriteLine("3. Withdraw money");
                    Console.WriteLine("4. sign out");
                    int option;
                    while (!int.TryParse(Ask("Choose a one of the options by write the number of the option"),
                               out option))
                    {
                        Console.WriteLine("invalid choice");
                    }

                    switch (option)
                    {
                        case 1:
                            menuNeedEnter = true;
                            if (user != null)
                            {
                                AccountsBalance(user, ballances);
                            }

                            break;
                        case 2:
                            menuNeedEnter = true;
                            if (user != null)
                            {
                                TransferBetweenAccounts(user, ballances);
                            }

                            break;
                        case 3:
                            menuNeedEnter = true;
                            if (user != null)
                            {
                                WithdrawMoney(user, ballances);
                            }
                            break;
                        case 4:
                            Console.WriteLine("sign out");
                            menuNeedEnter = false;
                            SignOut(user);
                            user = SignIn(users);
                            break;
                        default:
                            Console.WriteLine("invalid choice");
                            break;
                    }
                }
            }
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
            Console.Clear();
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

        private static void AccountsBalance(string[] user, string[][] ballances)
        {
            Console.Clear();

            string[][] userBallance = GetUserBallance(user, ballances);

            foreach (var ballance in userBallance)
            {
                Console.WriteLine($"Account {ballance[1]} balance: \u001b[33m{ballance[2]}{ballance[3]}\u001b[0m");
            }
        }

        private static void TransferBetweenAccounts(string[] user, string[][] ballances)
        {
            string[][] userBallances = GetUserBallance(user, ballances);

            if (userBallances.Length < 2)
            {
                Console.WriteLine("You need at least two accounts to make this action.");
                return;
            }

            Console.WriteLine("Transfer between accounts");

            for (int i = 0; i < userBallances.Length; i++)
            {
                var userBallance = userBallances[i];

                Console.WriteLine($"{i + 1} {userBallance[1]} with {userBallance[2]}{userBallance[3]}");
            }

            int transferFrom;
            while (
                !int.TryParse(Ask("Chose a acount to transefer from. Only the coresponding number works"),
                    out transferFrom)
                || userBallances.Length < transferFrom
            )
            {
                Console.WriteLine("You try to choose an account outside the given boundaries");
            }

            int transferTo;
            while (
                !int.TryParse(Ask("Chose a acount to transefer to. Only the coresponding number works"), out transferTo)
                || transferTo == transferFrom
                || userBallances.Length < transferTo
            )
            {
                Console.WriteLine(
                    "You try to choose an account outside the given boundaries or the same as you the transfer from");

                for (int i = 0; i < userBallances.Length; i++)
                {
                    var userBallance = userBallances[i];

                    Console.WriteLine($"{i + 1} {userBallance[1]} with {userBallance[2]}{userBallance[3]}");
                }
            }

            double transferAmount;
            while (!double.TryParse(
                       Ask(
                           $"How mouth do you want to transefer betwine {userBallances[transferFrom][1]} and {userBallances[transferTo - 1][1]}"),
                       out transferAmount)
                   || double.Parse(userBallances[transferFrom - 1][2]) < transferAmount
                  )
            {
                Console.WriteLine($"You can´t transfer more then {userBallances[transferFrom - 1][2]}");
            }

            Console.WriteLine($"You transfer {transferAmount}");

            userBallances[transferFrom - 1][2] =
                (double.Parse(userBallances[transferFrom - 1][2]) - transferAmount).ToString("#.00");
            userBallances[transferTo - 1][2] =
                (double.Parse(userBallances[transferTo - 1][2]) + transferAmount).ToString("#.00");
            Console.WriteLine("the new ballans is");

            for (int i = 0; i < userBallances.Length; i++)
            {
                var userBallance = userBallances[i];

                Console.WriteLine($"{userBallance[1]} with {userBallance[2]}{userBallance[3]}");
            }
        }

        private static void WithdrawMoney(string[] user, string[][] ballances)
        {
            string[][] userBallances = GetUserBallance(user, ballances);

            for (int i = 0; i < userBallances.Length; i++)
            {
                var userBallance = userBallances[i];

                Console.WriteLine($"{i + 1} {userBallance[1]} with {userBallance[2]}{userBallance[3]}");
            }

            int transferFrom;
            while (!int.TryParse(Ask("chose a acount to transfer from"), out transferFrom) || userBallances.Length < transferFrom || transferFrom < 1)
            {
                Console.WriteLine("You try to choose an account outside the given boundaries");
                for (int i = 0; i < userBallances.Length; i++)
                {
                    var userBallance = userBallances[i];

                    Console.WriteLine($"{i + 1} {userBallance[1]} with {userBallance[2]}{userBallance[3]}");
                }
            }

            double transferAmount;
            while (!double.TryParse(
                       Ask(
                           $"How mouth do you want to transefer from {userBallances[transferFrom - 1][1]}. You can´t tresfer more then {userBallances[transferFrom - 1][2]}{userBallances[transferFrom - 1][3]}"),
                       out transferAmount)
                   || double.Parse(userBallances[transferFrom - 1][2]) < transferAmount
                  )
            {
                Console.WriteLine($"You can´t transfer more then {userBallances[transferFrom - 1][2]}");
            }

            /* If the user is not found givs tow more trays */
            for (int i = 0; i < 3; i++)
            {
                int pin;
                while (!int.TryParse(Ask("Write your pin to conferme the transaktion"), out pin))
                {
                    Console.WriteLine("Invalid pin, try again");
                }

                /* User enter wrong pin code and for-loop is not on last round */
                if (int.Parse(user[2]) != pin && i != 2)
                {
                    Console.WriteLine("Wrong pin, try again");
                    continue;
                }

                /* User enter wrong pin code and for-loop on last round */
                if (int.Parse(user[2]) != pin && i == 2)
                {
                    return;
                }

                /* If the user enters the correct pin code */
                break;
            }

            Console.WriteLine("whedraing the many");

            double transferFromAmount = double.Parse(userBallances[transferFrom - 1][2]);

            userBallances[transferFrom - 1][2] =
                (transferFromAmount - transferAmount).ToString("#.00");

            Console.WriteLine($"The new value on the acount is {userBallances[transferFrom - 1][2]}{userBallances[transferFrom - 1][3]}");
        }

        /// <summary>
        /// Get the user ballance.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="ballances"></param>
        /// <returns>empty array if user not have any ballance</returns>
        private static string[][] GetUserBallance(string[] user, string[][] ballances)
        {
            string[][] userBallance = [];

            for (int i = 0; i < ballances.GetLength(0); i++)
            {
                if (int.Parse(ballances[i][0]) == int.Parse(user[0]))
                {
                    Array.Resize(ref userBallance, userBallance.Length + 1);
                    userBallance[userBallance.Length - 1] = ballances[i];
                }
            }

            if (userBallance.Length == 0)
            {
                Console.WriteLine("Account does not exist");
            }

            return userBallance;
        }
    }
}