using System;
using System.Linq;

namespace Yahtzee
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var (numberDice, numberSides, numberRounds) = InputNumber();

            Console.WriteLine("Run with LinQ, press key 1:");
            Console.WriteLine("Run with array sort method, press key any:");

            int.TryParse(Console.ReadLine(), out int checkKey);
            if (checkKey == 1)
            {
                RunWithLinq(numberDice, numberSides, numberRounds);
            }
            else
            {
                RunWithSort(numberDice, numberSides, numberRounds);
            }

            Console.ReadKey();
        }

        private static (int, int, int) InputNumber()
        {
            // Input number dice, sides, rounds
            int numberDice, numberSides, numberRounds;

            Console.WriteLine("Input number dice:");
            while (!int.TryParse(Console.ReadLine(), out numberDice))
            {
                Console.Write("Re-enter input number dice: ");
            }

            Console.WriteLine("Input number sides:");
            while (!int.TryParse(Console.ReadLine(), out numberSides))
            {
                Console.Write("Re-enter input number sides: ");
            }

            Console.WriteLine("Input number rounds:");
            while (!int.TryParse(Console.ReadLine(), out numberRounds))
            {
                Console.Write("Re-enter input number rounds: ");
            }

            return (numberDice, numberSides, numberRounds);
        }

        private static void RunWithLinq(int dice, int sides, int rounds)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var random = new Random();
            for (int round = 0; round < rounds; round++)
            {
                // Create variable to show result each round
                string resultOneRound = "[ ";

                // Initialize the array with every 1 rounds 
                int[] arrayOneRounds = new int[dice];
                for (int i = 0; i < dice; i++)
                {
                    arrayOneRounds[i] = random.Next(1, sides + 1);
                    resultOneRound = resultOneRound + arrayOneRounds[i] + " ";
                }

                var scoreOneRounds = arrayOneRounds.GroupBy(item => item).Max(group => group.Sum());
                resultOneRound = resultOneRound + "] => " + scoreOneRounds;
                PrintResult(resultOneRound);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Time run (milliseconds): {0}", elapsedMs);
        }

        private static void RunWithSort(int dice, int sides, int rounds)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var random = new Random();
            for (int round = 0; round < rounds; round++)
            {
                // Create variable to show result each round
                string resultOneRound = "[ ";

                // Initialize the array with every 1 rounds 
                int[] arrayOneRounds = new int[dice];
                for (int i = 0; i < dice; i++)
                {
                    arrayOneRounds[i] = random.Next(1, sides + 1);
                    resultOneRound = resultOneRound + arrayOneRounds[i] + " ";
                }

                // Sort array to calculate
                Array.Sort(arrayOneRounds);

                // Initialize the largest score as first place
                int scoreOneRounds = arrayOneRounds[0];

                // Total score of the same sides
                int totalSameSides = arrayOneRounds[0];
                for (int i = 0; i < dice - 1; i++)
                {
                    if (arrayOneRounds[i] == arrayOneRounds[i + 1])
                    {
                        totalSameSides = totalSameSides + arrayOneRounds[i];
                    }
                    else
                    {
                        // Check max score in Sides
                        if (scoreOneRounds < totalSameSides)
                        {
                            scoreOneRounds = totalSameSides;
                        }

                        totalSameSides = arrayOneRounds[i + 1];
                    }
                }

                // Check max score in Sides
                if (scoreOneRounds < totalSameSides)
                {
                    scoreOneRounds = totalSameSides;
                }

                resultOneRound = resultOneRound + "] => " + scoreOneRounds;
                PrintResult(resultOneRound);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Time run (milliseconds): {0}", elapsedMs);
        }

        private static void PrintResult(string result)
        {
            Console.WriteLine(result);
        }
    }
}
