using assignment2projectfinal;
using System;
using System.Diagnostics;

namespace assignment2projectfinal
{
    // Class responsible for testing various game functionalities.
    
    class Testing : Game
    {
        // No implementation needed for abstract member PlayGame
        public override int PlayGame() => 0;

   
        // Method to run tests based on user selection.
       
        public void RunTests()
        {
            while (true)
            {
                // Display test options to the user
                Console.WriteLine("Select an option:");
                Console.WriteLine("1. Test Roll Dices");
                Console.WriteLine("2. Test SevenOut");
                Console.WriteLine("3. Test ThreeOrMore");
                Console.WriteLine("4. Exit");

                string choice = Console.ReadLine(); // Get user input

                switch (choice)
                {
                    case "1":
                        TestRollDices();
                        break;
                    case "2":
                        TestSevenOut();
                        break;
                    case "3":
                        TestThreeOrMore();
                        break;
                    case "4":
                        return; // Exit the testing loop
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        // Method to test the rolling of dice.
       
        private void TestRollDices()
        {
            SevenOut game = new SevenOut(); // Create a SevenOut game instance.
            game.CreateDie(3); // Create 3 dice.
            int[] rolls = game.RollDie(); // Roll the dice and capture the results.

            // Ensure each die roll is within the valid range (1-6).
            foreach (var roll in rolls)
            {
                Debug.Assert(roll >= 1 && roll <= 6, $"Test Failed: {roll}. Roll should be between 1 and 6.");
            }

            Console.WriteLine("Test Roll Dices Passed."); // Confirm the test passed if no assertions failed.
        }

        // Method to test the SevenOut game.
     
        private void TestSevenOut()
        {
            SevenOut game = new SevenOut();

            game.PlayGame();  // This assumes you have a way to mock dice roll outcomes

            int total = game.PlayGame();
            Debug.Assert(total == 7, "Test Failed: Game should have stopped with a total sum of 7.");
            Console.WriteLine("Test SevenOut Passed.");
        }

         // Method to test the ThreeOrMore game.
      
        private void TestThreeOrMore()
        {
            ThreeOrMore game = new ThreeOrMore();

            game.TestGame();  // This also assumes methods to mock scores

            game.PlayGame();
            Debug.Assert(game.TotalPoints >= 20, "Test Failed: Total points should be 20 or more.");
            Console.WriteLine("Test Three Or More Passed.");
        }
    }
}
