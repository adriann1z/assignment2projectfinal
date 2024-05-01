using System;
using assignment2projectfinal; // Importing the namespace where the SevenOut class resides

namespace assignment2projectfinal
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate a SevenOut game object
            SevenOut sevenOut = new SevenOut();

            // Display a welcome message and instructions
            Console.WriteLine("Welcome to the dice game!");
            Console.WriteLine("Press any key to start the game...");
            Console.ReadKey();

            // Start the game by displaying the main menu
            sevenOut.GameMenu();

            // After the game finishes, display a farewell message
            Console.WriteLine("Thank you for playing the dice games. Goodbye!");
        }
    }
}
