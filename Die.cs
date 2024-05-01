using System;

namespace assignment2projectfinal
{
    class Die
    {
        private static Random random = new Random(); // Static Random instance for generating random numbers

        public int CurrentValue { get; private set; }

        public Die()
        {
            Roll(); // Roll the die to generate an initial value
        }

        public int Roll()
        {
            CurrentValue = random.Next(1, 7); // Generates a random number between 1 and 6 (inclusive)
            return CurrentValue; // Returns the rolled value
        }
    }
}
