# Cash machine
This is a school project where I would learn .Net how to manipulate data. The task was to make a console app where you can log in as a user and then be able to do a few things such as being able to see accounts and balances, transfer between accounts and withdraw money. We had a limitation in this project. Limitation was that we were not allowed to use any kind of classes and certain methods to search through an Array a little easier.
# How to run the application
1. Open the project in a console
2. Run the dotnet run command
3. cash machine is now running
    1. Username you can choose from Mickey, Donald Duck, Goofy, Pluto or Scar. Be sure to spell it the same way.
    2. The pin code is the same for all users, "1234".
4. Follow the instructions in the console
Code structure
Cash machinehas several different virtual pages (login, view accounts and balance, transfer between accounts and withdraw money) that are neatly separated into their own method in the [Program.cs](/Program.cs) file. There are ready-made users and bank accounts sitting at the top of the [Program.cs](/Program.cs) file. The finished users and bank accounts are like a local database saved in two jagged arrays.
# Reflections
There are things that I could have done better and there are things that I think I could not have solved in any other way with the limitations that I was given.
- In some places in cash machine, the user must choose between one or more alternatives where you enter which number to choose the right alternative. I chose to do it because it was a quick fix to be able to focus more on the other things. This could be redone so that you can use the up and down arrow keys. Which I think is a better solution.
- When you start the console, you could get a better welcome with an ASCII art that says welcome to the bank. Now it's just a text that says, "Welcome to the Jos cash machine".
- How I save my bank accounts could be changed so that account type and money can be in two different jagged arrays. This means that money can be saved in double and I don't have to redo it from text to double and vice versa. Which makes it possible to write code that is easier to read.