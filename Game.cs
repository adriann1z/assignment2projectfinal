using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace assignment2projectfinal
{
    interface IRollable
    {
        int Roll();
    }

    abstract class Game //Implements the IRollable interface
    {
        protected List<Die> dices = new List<Die>(); //protected list of Die objects

        public abstract int PlayGame(); //Abstract method to play the game

        public Statistics Statistics { get; } = new Statistics(); //Property to hold the statistics object

        public virtual void CreateDie(int count) //Method to create dice objects
        {
            dices.Clear(); //Clears the existing dices list
            for (int i = 0; i < count; i++) //Loops through the array 3 times
            {
                dices.Add(new Die()); //Adds a new dice object to the dices list
            }
        }

        public virtual int[] RollDie() //Method to roll three dice objects
        {
            List<int> rolls = new List<int>(); // List to hold the rolls
            int totalSum = 0;

            foreach (var die in dices)  //Loops through the dices array
            {
                int roll = die.Roll(); // Roll each die
                totalSum += roll;
                rolls.Add(roll);

                Console.WriteLine($"Dice: {roll}"); //Prints the roll of each dice object
            }

            Statistics.IncrementTotalPlays(); //Calls the IncrementTotalPlays method
            Statistics.UpdateStatistics(totalSum); //Calls the UpdateStatistics method
            Console.WriteLine($"Total Sum: {totalSum}"); //Prints the total sum of the rolls
            return rolls.ToArray();   //Returns the rolls of the three dice objects
        }

        public void ContinousDice() //Method to roll the dice continuously
        {
            bool continueRolling = true; //Variable to hold the state of the loop

            Console.WriteLine("Press 'r' to roll or 's' to exit.");

            while (continueRolling)
            {
                string userInput = Console.ReadLine().ToLower();

                switch (userInput)
                {
                    case "r":
                        CreateDie(3); //Calls the createDie method
                        RollDie(); //Calls the RollDie method

                        Console.WriteLine("Press 'r' to roll again or 's' to stop.");
                        break;
                    case "s":
                        continueRolling = false; //Sets continueRolling to false
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again."); //Prints an error message
                        Console.WriteLine("Press 'r' to roll or 's' to exit.");
                        break;
                }
            }
            Statistics.IncrementTotalPlays(); //Calls the IncrementTotalPlays method
        }

        public void GameMenu()
        {
            bool exit = false; // Flag to control the loop

            while (!exit) // Keep showing the menu until the user chooses to exit
            {
                Console.WriteLine("1. Play SevenOut.");
                Console.WriteLine("2. Play Three OR More.");
                Console.WriteLine("3. Perform Testing.");
                Console.WriteLine("4. View Statistics.");
                Console.WriteLine("5. Roll 3 Dice Game.");
                Console.WriteLine("6. Exit."); // Add an option to exit the program

                Console.WriteLine("Select an option:");

                int choice;
                bool isValid = int.TryParse(Console.ReadLine(), out choice); //Takes the user input and stores it in the choice variable
                if (!isValid) //Checks if the input is valid
                {
                    Console.WriteLine("Invalid choice. Please enter a number.");
                    continue; // Skip the rest of the loop and show the menu again
                }

                switch (choice)
                {
                    case 1:
                        SevenOut sevenOut = new SevenOut(); //Instantiate the SevenOut class
                        sevenOut.PlayGame(); //Calls the Play method
                        break;
                    case 2:
                        ThreeOrMore threeOrMore = new ThreeOrMore(); //Instantiate the threeOrMore class
                        threeOrMore.GameMenu(); //Calls the GameMenu method
                        break;
                    case 3:
                        Console.WriteLine("Performing Testing...");
                        // You can add testing functionality here
                        break;
                    case 4:
                        Statistics.DisplayStats(); //Calls the DisplayStats method directly from the Statistics instance
                        break;
                    case 5:
                        Console.WriteLine("Rolling Three Die:");
                        CreateDie(3); //Calls the createDie method
                        RollDie(); //Calls the RollThree method

                        ContinousDice(); //Calls the ContinousDice method
                        break;
                    case 6:
                        Console.WriteLine("Exiting...");
                        exit = true; // Set the flag to true to exit the loop and end the program
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again."); //Prints an error message
                        break;
                }

                // Optional: Add a pause here so the user can see the result before the menu shows again
                if (!exit) // Only pause if we're not exiting
                {
                    Console.WriteLine("Press any key to return to the menu...");
                    Console.ReadKey();
                    Console.Clear(); // Clear the console for a clean menu display
                }
            }
        }
    }

    class SevenOut : Game, IRollable //Inherits from the Game class and implements the IRollable interface
    {
        public override int PlayGame()
        {
            Console.WriteLine("1. Player vs Player");
            Console.WriteLine("2. Player vs Computer");
            Console.WriteLine("3. Exit");

            string choice;
            bool isValidChoice = false;
            do
            {
                choice = Console.ReadLine();
                isValidChoice = choice == "1" || choice == "2" || choice == "3"; // Check if the input is valid
                if (!isValidChoice)
                {
                    Console.WriteLine("Invalid input. Please select 1, 2, or 3.");
                }
            } while (!isValidChoice); // Keep asking for input until it's valid

            if (choice == "3")
            {
                Console.WriteLine("Exiting game...");
                return 0; // Exit the game early if the user chooses to exit
            }

            bool isPlayerTurn = true; // Track turn in player vs computer mode
            RollTwo(); // Calls the RollTwo method

            int total = 0; // Variable to hold the total sum of the rolls
            int rollCount = 0; // Variable to hold the number of rolls
            string userInput = "";

            Console.WriteLine("Press 'r' to roll or 's' to exit.");

            while (true)
            {
                if (choice == "2" && !isPlayerTurn)
                {
                    Console.WriteLine("Computer's turn to roll...");
                    Thread.Sleep(1000); // Adding delay to simulate computer's turn
                    userInput = "r"; // Assume computer always rolls
                }
                else
                {
                    Console.WriteLine("Your turn to roll...");
                    userInput = Console.ReadLine().ToLower();
                }

                switch (userInput)
                {
                    case "r":
                        int rollTotal = RollTwoDice(); // Calls the RollDice method and stores the return value in the total variable
                        rollCount++;

                        if (rollTotal == 7) // Checks if the roll is 7
                        {
                            Console.WriteLine($"{(isPlayerTurn ? "You" : "Computer")} rolled a 7. {(isPlayerTurn ? "You" : "Computer")} Lost!"); // Displays who lost if the roll is 7
                            Console.WriteLine("Game Over!");

                            Statistics.IncrementTotalPlays(); // Calls the IncrementTotalPlays method
                            Statistics.UpdateStatistics(total); // Calls the UpdateStatistics method
                            Console.WriteLine($"Total Rolls: {rollCount}"); // Prints the total number of rolls
                            return total;
                        }
                        else
                        {
                            total += rollTotal;
                        }

                        Console.WriteLine($"Total: {total}"); // Prints the total sum of the rolls

                        if (choice == "1" || isPlayerTurn) // Only prompt again if it's player vs player or player's turn
                        {
                            Console.WriteLine("Press 'r' to roll again or 's' to stop.");
                        }

                        if (choice == "2") isPlayerTurn = !isPlayerTurn;
                        break;
                    case "s":
                        if (isPlayerTurn || choice == "1")
                        {
                            Statistics.IncrementTotalPlays(); // Calls the IncrementTotalPlays method
                            Statistics.UpdateStatistics(total); // Calls the UpdateStatistics method
                            return total;// Returns the total sum of the rolls
                        }
                        break;
                    default:
                        if (isPlayerTurn) // Only show the invalid message if it's the player's turn
                        {
                            Console.WriteLine("Invalid choice. Please press 'r' to roll or 's' to exit.");
                        }
                        break;
                }
            }
        }


        private void RollTwo() //Method to create two dice objects
        {
            dices = new List<Die>(); //Creates a new array of Die objects with a length of 2

            for (int i = 0; i < 2; i++) //Loops through the array 2 times
            {
                dices.Add(new Die()); //Creates two dice objects and stores them in a list
            }

            return;
        }

        public int RollTwoDice()
        {
            int total = 0; //Variable to hold the total sum of the rolls
            bool rolledDouble = false; //Variable to hold the state of the double roll

            foreach (var die in dices) //Loops through the dices array
            {
                int result = die.Roll(); //Calls the Roll method on each dice object
                total += result; //Adds the roll to the total
                Console.WriteLine($"Dice: {result}"); //Prints the roll of each dice object
            }

            int[] currentValue = dices.Select(die => die.CurrentValue).ToArray(); // Linq query to get the current value of the dice objects

            if (currentValue[0] == currentValue[1]) //Checks if the two dice have the same value
            {
                rolledDouble = true; //Sets doubleRoll to true if the two dice have the same value
                Console.WriteLine("You rolled a double!");
            }

            return rolledDouble ? total * 2 : total; //Returns the total sum of the rolls multiplied by 2 if the two dice have the same value
        }

        public int Roll()
        {
            return RollTwoDice();
        }
    }

    class ThreeOrMore : Game
    {
        public int TotalPoints { get; private set; } = 0;

        public override int PlayGame()
        {
            int totalScore = 0;
            int rounds = 5; // Number of rounds to play

            for (int i = 0; i < rounds; i++)
            {
                CreateDie(5); // Create 5 dice
                int[] rolls = RollDie(); // Roll them and get results
                int roundScore = CalculatePoints(rolls); // Calculate points based on dice values
                totalScore += roundScore;
                Console.WriteLine($"Round {i + 1}: You scored {roundScore} points.");
            }

            Statistics.IncrementTotalPlays();
            Statistics.UpdateStatistics(totalScore);
            return totalScore; // Return the total score after all rounds
        }

        public void TestGame()
        {
            TotalPoints = 0; // Total points for the test

            int[] optimalRoll = new int[] { 6, 6, 6, 6, 6 }; // Simulate a 5-of-a-kind for maximum points

            while (TotalPoints < 20) // Play until 20 points are reacheds
            {
                TotalPoints += CalculatePoints(optimalRoll); // Calculate points for the optimal roll
            }
        }
        public void GameMenu()
        {
            try
            {
                // Display menu options
                Console.WriteLine("1. Player vs Player");
                Console.WriteLine("2. Player vs Computer");
                Console.WriteLine("3. Exit");

                int choice;
                // Get user input for menu choice
                if (!int.TryParse(Console.ReadLine(), out choice))
                {
                    // Throw an exception if user input cannot be parsed to an integer
                    throw new ArgumentException("Invalid input. Please enter a number.");
                }

                // Switch case based on user's menu choice
                switch (choice)
                {
                    case 1:
                        // Start a new game against the computer
                        ThreeOrMore threeOrMoreVsComputer = new ThreeOrMore();
                        threeOrMoreVsComputer.Play(false);
                        break;
                    case 2:
                        // Start a new game with another player
                        ThreeOrMore threeOrMoreVsPlayer = new ThreeOrMore();
                        threeOrMoreVsPlayer.Play(true);
                        break;
                    case 3:
                        // Exit the game
                        Console.WriteLine("Exiting...");
                        break;
                    default:
                        // Throw an exception for invalid menu choice
                        throw new ArgumentException("Invalid choice. Please enter a valid option.");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions and display error messages
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public void Play(bool playWithComputer)
        {
            CreateDie(5); // Create 5 dice for the game

            try
            {
                string player1Name = ""; // Player 1's name
                string player2Name = ""; // Player 2's name

                // Determine player names based on game mode
                if (playWithComputer)
                {
                    player1Name = "Player"; // Player's name
                    player2Name = "Computer"; // Computer's name
                }
                else
                {
                    player1Name = "Player 1"; // Player 1's name
                    player2Name = "Player 2"; // Player 2's name
                }

                int player1Score = 0; // Player 1's score
                int player2Score = 0; // Player 2's score
                bool gameInProgress = true; // Flag to indicate if the game is still in progress
                bool player1Turn = true; // Flag to indicate whose turn it is

                // Main game loop
                while (gameInProgress && (player1Score < 20 && player2Score < 20))
                {
                    // Display whose turn it is
                    Console.WriteLine($"Current turn: {(player1Turn ? player1Name : player2Name)}");
                    Console.WriteLine("Rolling 5 dice...");

                    // Initialize and roll 5 dice
                    CreateDie(5);
                    int[] rolls = RollDie();

                    // Calculate initial points
                    int points = CalculatePoints(rolls);
                    Console.WriteLine($"You scored {points} points this roll.");

                    // Check if two of a kind is rolled
                    if (rolls.GroupBy(x => x).Any(g => g.Count() == 2)) // Linq query to check if two of a kind is rolled
                    {
                        Console.WriteLine($"You rolled two of a kind. {points}");

                        // Player 1's turn against the computer
                        if (player1Turn && playWithComputer)
                        {
                            Console.WriteLine("Computer is deciding...");
                            // Computer's turn - reroll all dice
                            rolls = RollDie();
                            // Recalculate points after reroll
                            points = CalculatePoints(rolls);
                            Console.WriteLine($"Computer scored {points} points this turn.");
                        }
                        // Player's turn in other cases
                        else
                        {
                            Console.WriteLine("Do you want to reroll all dice (y) or only the non-matching ones (n)?");
                            string response = Console.ReadLine().ToLower();

                            // Validate reroll option
                            if (response != "y" && response != "n")
                            {
                                // Throw an exception for invalid input
                                throw new ArgumentException("Invalid input. Please enter 'y' or 'n'.");
                            }

                            if (response == "y")
                            {
                                // Reroll all dice
                                rolls = RollDie();
                                // Recalculate points after reroll
                                points = CalculatePoints(rolls);
                                Console.WriteLine($"After rerolling, you scored {points} points in total this turn.");
                            }
                            else if (response == "n")
                            {
                                // Reroll only the non-matching dice
                                CreateDie(rolls.Length - 2);
                                int[] nonMatchingRolls = RollDie();

                                // Replace the non-matching rolls in the original rolls array
                                int index = 0;
                                for (int i = 0; i < rolls.Length; i++)
                                {
                                    if (rolls.Count(r => r == rolls[i]) == 1)
                                    {
                                        rolls[i] = nonMatchingRolls[index++];
                                    }
                                }
                                // Recalculate points after reroll
                                points = CalculatePoints(rolls);
                                Console.WriteLine($"After rerolling, you scored {points} points in total this turn.");
                            }
                        }
                    }

                    // Update player's or computer's score
                    if (player1Turn)
                    {
                        player1Score += points; // Add points to player 1's score
                    }
                    else
                    {
                        player2Score += points; // Add points to player 2's score
                    }

                    // Display player's or computer's score
                    Console.WriteLine($"{player1Name} Score: {player1Score}");
                    Console.WriteLine($"{player2Name} Score: {player2Score}");

                    // Check if either player has won
                    if (player1Score >= 20 || player2Score >= 20)
                    {
                        // Declare the winner
                        if (player1Score >= 20)
                        {

                            Statistics.UpdateStatistics(player1Score);


                            Console.WriteLine($"Congratulations! {player1Name} has reached 20 points and won the game!");
                        }
                        else
                        {
                            Console.WriteLine($"Congratulations! {player2Name} has reached 20 points and won the game!");
                        }
                        gameInProgress = false; // End the game
                    }
                    else
                    {
                        // Prompt for next turn
                        Console.WriteLine("Press any key to continue to the next turn...");
                        Console.ReadKey();
                        Console.Clear(); // Clear the console for the next turn
                        player1Turn = !player1Turn; // Switch turns
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions and display error messages
                Console.WriteLine($"Error: {ex.Message}");
            }
        }


        private int CalculatePoints(int[] rolls)
        {
            var groups = rolls.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count()); // Linq query to group the rolls by number and count the occurrences
            int points = 0; // Variable to hold the total points

            foreach (var group in groups)
            {
                switch (group.Value) // group.Value is the count of occurrences of each dice number
                {
                    case 3:
                        points += 3; // Add 3 points for three of a kind
                        break;
                    case 4:
                        points += 6; // Add 6 points for four of a kind
                        break;
                    case 5:
                        points += 12; // Add 12 points for five of a kind
                        break;
                }
            }

            return points; // Return the total points
        }

    }

}
