using System;

namespace Rock_paper_scissors
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int userWins = 0;
            int computerWins = 0;
            string[] items = { "scissors", "rock", "paper" };
            Random random = new Random();

            Console.WriteLine("Select:");
            Console.WriteLine("Rock, paper or scissors?");

            while (true)
            {
                //chose item
                string input = Console.ReadLine().ToLower();

                int randomNumberToSelect = random.Next(2);
                // 0 - scissors
                // 1 - rock
                // 2 - paper

                if (input == "quit")
                {
                    break;
                }
                else if (input != items[0] && input != items[1] && input != items[2])
                {
                    Console.WriteLine("Invalid item");
                }
                else
                {
                    Console.WriteLine($"Computer picked {items[randomNumberToSelect]}");

                    if (input == "rock" && randomNumberToSelect == 0)
                    {
                        Console.WriteLine("You won!");
                        userWins++;
                    }
                    else if (input == "paper" && randomNumberToSelect == 1)
                    {
                        Console.WriteLine("You won!");
                        userWins++;
                    }
                    else if (input == "scissors" && randomNumberToSelect == 2)
                    {
                        Console.WriteLine("You won!");
                        userWins++;
                    }
                    else if (input == items[randomNumberToSelect])
                    {
                        Console.WriteLine("Draw");
                    }
                    else
                    {
                        Console.WriteLine("You lost! :(");
                        computerWins++;
                    }
                }
            }

            Console.WriteLine($"Computer wins: {computerWins}");
            Console.WriteLine($"Your wins: {userWins}");
            Console.WriteLine("Au revoir!");
        }
    }
}
