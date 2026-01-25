using System;
using System.Collections.Generic;

public class HelloWorld
{
    public static void Main(string[] args)
    {
        Random random = new Random();

        string word = "hello";
        string shuffledWord = "";

        List <int> shuffleIndexes = new List<int> { };

        for (int i = 0; i < word.Length; i++)
        {
            int shuffler = random.Next(0, word.Length);
            while (shuffleIndexes.Contains(shuffler))
            {
                shuffler = random.Next(0, word.Length);
            }
            shuffleIndexes.Add(shuffler);
            shuffledWord += word[shuffler];
        }


        Console.WriteLine(shuffledWord);

        Console.Write("Enter the word: ");
        string? inp = Console.ReadLine();

        if (inp == word)
        {
            Console.WriteLine("You got it right!!");
        }
        else
        {
            Console.WriteLine("You got it wrong");
        }
    }
}