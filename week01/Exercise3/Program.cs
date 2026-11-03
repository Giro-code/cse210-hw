using System;

class Program
{
    static void Main(string[] args)
    {
        Random generator = new Random();
        int magicNumber = generator.Next(1, 101);
        int guess;
        do
        {
            Console.Write("What is your guess? ");
            guess = int.Parse(Console.ReadLine());
            if (guess < magicNumber)
            {
                Console.WriteLine("Higher");
            }
            else if (guess > magicNumber)
            {
                Console.WriteLine("Lower");
            }
        } while (guess != magicNumber);
        Console.WriteLine("You guessed it!");
    }
}
