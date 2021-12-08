using System;
using System.Linq;

namespace Yahtzee
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Input number dice, sides, rounds
            int numberDice, numberSides, numberRounds;
            Console.WriteLine("Input number dice:");
            numberDice = InputNumber();

            Console.WriteLine("Input number sides:");
            numberSides = InputNumber();

            Console.WriteLine("Input number rounds:");
            numberRounds = InputNumber();

            Console.WriteLine("Run with unsorted array method, press key 1:");
            Console.WriteLine("Run with array sort method, press key any:"); 

            int.TryParse(Console.ReadLine(), out int checkKey);
            if (checkKey == 1)
            {
                RunWithNotSort(numberDice, numberSides, numberRounds);
            } 
            else
            {
                RunWithSort(numberDice, numberSides, numberRounds);
            }    
            
            Console.ReadKey();
        }

        private static int InputNumber()
        {
            int number;
            while (!int.TryParse(Console.ReadLine(), out number))
            {
                Console.Write("Re-enter input number: ");
            }

            return number;
        }

        private static void RunWithNotSort(int numberDice, int numberSides, int numberRounds)
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var random = new Random();
            for (int round = 0; round < numberRounds; round++)
            {
                // Create variable to show result each round
                string resultOneRound = "[ ";

                // Initialize the array with every 1 rounds 
                int[] arrayOneRounds = new int[numberDice];                
                for (int i = 0; i < numberDice; i++)
                {
                    arrayOneRounds[i] = random.Next(1, numberSides + 1);
                    resultOneRound = resultOneRound + arrayOneRounds[i] + " ";
                }

                // Initialize the largest score as first place
                int scoreOneRounds = arrayOneRounds[0];

                // Total score of the same sides
                int totalSameSides = arrayOneRounds[0];

                // Initialize sub-array mark the counted dice
                int[] subArray = new int[numberDice];
                for (int i = 0; i < numberDice; i++)
                {
                    // Check element already running
                    if(!subArray.Contains(arrayOneRounds[i]))
                    {
                        subArray.Append(arrayOneRounds[i]);
                        totalSameSides = arrayOneRounds[i];
                        for (int j = i + 1; j < numberDice; j++)
                        {
                            if (arrayOneRounds[i] == arrayOneRounds[j])
                            {
                                totalSameSides = totalSameSides + arrayOneRounds[i];
                            }
                        }

                        // Check max score in Sides
                        if (scoreOneRounds < totalSameSides)
                        {
                            scoreOneRounds = totalSameSides;
                        }
                    }   
                }

                resultOneRound = resultOneRound + "] => " + scoreOneRounds;
                Console.WriteLine(resultOneRound);
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
                Console.WriteLine(resultOneRound);
            }

            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine("Time run (milliseconds): {0}", elapsedMs);
        }
    }
}
