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
            string[,][][] accounts =
            {
                {
                    [
                        ["lönekonto", "3456,78", "kr"],
                    ],
                },
                {
                    [
                        ["lönekonto", "1234,56", "kr"],
                        ["sparkonto", "7890,12", "kr"],
                    ]
                },
                {
                    [
                        ["sparkonto", "30608,00", "kr"],
                        ["sparkonto", "3456,78", "kr"],
                        ["Hyreskonto", "7890,12", "kr"],
                    ]
                },
                {
                    [
                        ["lönekonto", "1234,56", "kr"],
                        ["sparkonto", "5678,90", "kr"],
                        ["Hyreskonto", "9876,34", "kr"],
                        ["Matbudgeten", "2134,78", "kr"],
                    ]
                },
                {
                    [
                        ["lönekonto", "8901,12", "kr"],
                        ["sparkonto", "4567,56", "kr"],
                        ["Hyreskonto", "6789,90", "kr"],
                        ["Matbudgeten", "1234,34", "kr"],
                        ["Nödfonden", "5678,78", "kr"]
                    ]
                }
            };



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
                /* If user is not sign in break out the loop */
                if (user == null)
                {
                    return;
                }

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

                    // Console.Clear();
                    switch (option)
                    {
                        case 1:
                            menuNeedEnter = true;
                            AccountsBalance(user, accounts);

                            break;
                        case 2:
                            menuNeedEnter = true;
                            TransferBetweenAccounts(user, accounts);
                            break;
                        case 3:
                            menuNeedEnter = true;
                            WithdrawMoney(user, accounts);
                            break;
                        case 4:
                            Console.WriteLine("sign out");
                            menuNeedEnter = false;
                            user = SignIn(users);
                            break;
                        default:
                            Console.WriteLine("invalid choice");
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Sign in the user.
        /// </summary>
        /// <param name="users"></param>
        /// <returns>Returns a user if the user uses the correct credentials otherwise null.</returns>
        private static string[]? SignIn(string[][] users)
        {
            // Console.Clear();
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

                /* if user exist return the user */
                if (user != null)
                {
                    return user;
                }

                Console.WriteLine("Wrong name or pin, try again");
            }

            return user;
        }

        /// <summary>
        /// Show the accounts and balance.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="accounts"></param>
        private static void AccountsBalance(string[] user, string[,][][] accounts)
        {
            Console.Clear();

            string[][] userAccounts = GetUserAccounts(user, accounts);

            foreach (var userAccount in userAccounts)
            {
                Console.WriteLine(
                    $"Account {userAccount[0]} balance: \u001b[33m{userAccount[1]}{userAccount[2]}\u001b[0m");
            }
        }

        /// <summary>
        /// Transfer between accounts.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="accounts"></param>
        private static void TransferBetweenAccounts(string[] user, string[,][][] accounts)
        {
            string[][] userAccounts = GetUserAccounts(user, accounts);

            if (userAccounts.Length < 2)
            {
                Console.WriteLine("You need at least two accounts to make this action.");
                return;
            }

            Console.WriteLine("Transfer between accounts");

            for (int i = 0; i < userAccounts.Length; i++)
            {
                var userBallance = userAccounts[i];

                Console.WriteLine($"{i + 1} {userBallance[0]} with {userBallance[1]}{userBallance[2]}");
            }

            int transferFrom;
            while (
                !int.TryParse(Ask("Chose a acount to transefer from. Only the coresponding number works"),
                    out transferFrom)
                || transferFrom < 1
                || userAccounts.Length < transferFrom
            )
            {
                Console.WriteLine("You try to choose an account outside the given boundaries");
            }

            int transferTo;
            while (
                !int.TryParse(Ask("Chose a acount to transefer to. Only the coresponding number works"),
                    out transferTo)
                || transferTo < 1
                || transferTo == transferFrom
                || userAccounts.Length < transferTo
            )
            {
                Console.WriteLine(
                    "You try to choose an account outside the given boundaries or the same as you the transfer from");

                for (int i = 0; i < userAccounts.Length; i++)
                {
                    var userBallance = userAccounts[i];

                    Console.WriteLine($"{i + 1} {userBallance[0]} with {userBallance[1]}{userBallance[2]}");
                }
            }

            /* formats the number into a Swedish format  */
            NumberFormatInfo numberFormat = new CultureInfo("sv-SE").NumberFormat;
            double transferFromAmount = double.Parse(userAccounts[transferFrom - 1][1], numberFormat);
            double transferToAmount = double.Parse(userAccounts[transferTo - 1][1], numberFormat);

            double transferAmount;
            while (!double.TryParse(
                       Ask(
                           $"How mouth do you want to transefer betwine {userAccounts[transferFrom - 1][1]} and {userAccounts[transferTo - 1][1]}"),
                       out transferAmount)
                   || transferAmount < 0
                   || transferFromAmount < transferAmount
                  )
            {
                Console.WriteLine($"You can´t transfer more then {userAccounts[transferFrom - 1][1]}");
            }

            Console.WriteLine($"You transfer {transferAmount}");


            /* Format back from decimal to string with two decimal places */
            userAccounts[transferFrom - 1][1] = (transferFromAmount - transferAmount).ToString("n2", numberFormat);
            userAccounts[transferTo - 1][1] = (transferToAmount + transferAmount).ToString("n2", numberFormat);

            Console.WriteLine("the new ballans is");

            for (int i = 0; i < userAccounts.Length; i++)
            {
                var userBallance = userAccounts[i];

                Console.WriteLine($"{userBallance[1]} with {userBallance[2]}{userBallance[3]}");
            }
        }

        /// <summary>
        /// Withdraw money from the account.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="accounts"></param>
        private static void WithdrawMoney(string[] user, string[,][][] accounts)
        {
            string[][] userAccounts = GetUserAccounts(user, accounts);

            for (int i = 0; i < userAccounts.Length; i++)
            {
                var userAccount = userAccounts[i];

                Console.WriteLine($"{i + 1} {userAccount[0]} with {userAccount[1]}{userAccount[2]}");
            }

            int transferFrom;
            while (!int.TryParse(Ask("chose a acount to transfer from"), out transferFrom)
                   || userAccounts.Length < transferFrom
                   || transferFrom < 1)
            {
                Console.WriteLine("You try to choose an account outside the given boundaries");
                for (int i = 0; i < userAccounts.Length; i++)
                {
                    var userBallance = userAccounts[i];

                    Console.WriteLine($"{i + 1} {userBallance[0]} with {userBallance[1]}{userBallance[2]}");
                }
            }

            NumberFormatInfo numberFormat = new CultureInfo("sv-SE").NumberFormat;
            double transferFromAmount = double.Parse(userAccounts[transferFrom - 1][1], numberFormat);

            double transferAmount;
            while (!double.TryParse(
                       Ask(
                           $"How mouth do you want to transefer from {userAccounts[transferFrom - 1][1]}. You can´t tresfer more then {transferFromAmount}{userAccounts[transferFrom - 1][2]}"),
                       out transferAmount)
                   || transferAmount < 0
                   || transferFromAmount < transferAmount
                  )
            {
                Console.WriteLine($"You can´t transfer more then {transferFromAmount}");
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

            /* Format back from decimal to string with two decimal places */
            userAccounts[transferFrom - 1][2] = (transferFromAmount - transferAmount).ToString("n2", numberFormat);

            Console.WriteLine(
                $"The new value on the acount is {userAccounts[transferFrom - 1][1]}{userAccounts[transferFrom - 1][2]}");
        }

        /// <summary>
        /// Get the user accounts.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="accounts"></param>
        /// <returns>empty array if user not have any accounts</returns>
        private static string[][] GetUserAccounts(string[] user, string[,][][] accounts)
        {
            int userId = int.Parse(user[0]) - 1;

            var userAccounts = accounts[userId, 0];

            if (userAccounts.Length == 0)
            {
                Console.WriteLine("Account does not exist");
            }

            return userAccounts;
        }

        /// <summary>
        /// combines the Console.WriteLine and Console.ReadLine into one method.
        /// </summary>
        /// <param name="question"></param>
        /// <returns></returns>
        private static string Ask(string question)
        {
            Console.WriteLine(question);
            return Console.ReadLine() ?? "";
        }
    }
}