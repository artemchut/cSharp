using System;
using System.Collections.Generic;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            List<String> wordList = new List<String> {"a", "p", "p", "l", "e"};
            String word = String.Join("", wordList);
            String inp = "";
            bool won = false;
            int tries = 1;
            List<String> userCorrect = new List<String> {"_", "_" , "_", "_" , "_" };

            while (won == false && tries < 6)
            {
                tries++;
                for (int i = 0; i < word.Length; i++)
                {
                    userCorrect[i] = "_";
                }
                Console.Write("Enter ur guess: ");
                inp = Console.ReadLine();
                while (inp.Length != 5)
                {
                    Console.WriteLine("Gotta be 5 letters long");
                    Console.Write("Enter ur guess: ");
                    inp = Console.ReadLine();
                }
                if (inp == word)
                {
                    won = true;
                    Console.WriteLine("You won!!");
                    Console.WriteLine($"The word was: {word}");
                    break;
                }
                for (int i = 0; i < word.Length; i++)
                {
                    if (inp[i] == word[i])
                    {
                        userCorrect[i] = wordList[i];
                    }
                }
                Console.WriteLine(string.Join("", userCorrect));
            }
            if (won == false)
                {
                    Console.WriteLine("You lost, lol");
                }
        }
    }
}