using System;
using System.Collections.Generic;
using System.IO;

namespace assignment2projectfinal
{
    public class Statistics
    {
        public int TotalPlays { get; private set; } = 0; // Initialize TotalPlays to 0
        public int TotalScore { get; private set; } = 0; // Initialize TotalScore to 0
        private readonly string statsFile = "stats.txt"; // File to save statistics

        public void IncrementTotalPlays()
        {
            TotalPlays++; // Increment TotalPlays after each game is played
        }

        public void UpdateStatistics(int score)
        {
            TotalScore += score; // Update TotalScore with the latest score
        }

        public void SaveStatistics()
        {
            string solutionDirectory = AppDomain.CurrentDomain.BaseDirectory; // Get the solution directory
            string fullPath = Path.Combine(solutionDirectory, statsFile); // Combine the solution directory with the stats file

            List<string> stats = new List<string>() // Create a list of statistics to save to the file
            {
                $"TotalPlays: {TotalPlays}",
                $"TotalScore: {TotalScore}"
            };

            try
            {
                using (StreamWriter writer = new StreamWriter(fullPath))
                {
                    foreach (string stat in stats)
                    {
                        writer.WriteLine(stat); // Write each statistic to the file
                    }
                }

                Console.WriteLine($"Statistics saved to file at: {fullPath}"); // Display the full path to the file
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving statistics to file: {ex.Message}");
            }
        }

        public void DisplayStats()
        {
            Console.WriteLine("Would you like to save the statistics to a file? (Y/N)");

            string choice = Console.ReadLine().ToLower();

            if (choice == "y")
            {
                SaveStatistics();
                Console.WriteLine("Statistics saved to file... Check file directory!");
            }
            else
            {
                Console.WriteLine($"TotalPlays: {TotalPlays}, TotalScore: {TotalScore}");
                Console.WriteLine("No statistics saved to file.");
            }
        }
    }
}
