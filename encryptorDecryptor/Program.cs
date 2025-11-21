using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;

namespace encrypter
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            List<char> list = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',
                                               'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            List<char> usedLetters = new List<char> { };
            int sameLetters = 0;
            List<String> encrKey = new List<String> { };

            Console.Write("To encr/decr?: ");
            String? encrOpt = Console.ReadLine().ToLower(); 

            int stepFirst = random.Next(1, 26);
            while (stepFirst == 13)
            {
                stepFirst = random.Next(1, 26);
            }
            int step = stepFirst;

            if (encrOpt == "encr")
            {
                String encriptedWord = "";
                Console.Write("Enter a word to be encripted: ");
                String? encrWord = Console.ReadLine().ToLower();
                encrKey.Add(stepFirst.ToString());

                for (int i = 0; i < encrWord.Length; i++)
                {
                    //if char is not a letter
                    if (!list.Contains(encrWord[i]))
                    {
                        encriptedWord += encrWord[i];
                        continue;
                    }
                    //when sameLetters==stepFirst-start from the very start to keep it more secure(not the same pattern)
                    if (sameLetters > 0)
                    {
                        if (sameLetters%stepFirst==0)
                        {
                            sameLetters=0;
                            usedLetters.Clear();
                        }   
                    }

                    //shift by twice as much when the char appears for the second++ time
                    if (usedLetters.Contains(encrWord[i]))
                    {
                        sameLetters++;
                        for (int k = 0; k < sameLetters; k++)
                        {
                            step += stepFirst;
                        }
                        encrKey.Add(i.ToString());
                    }

                    //adding every new letter to a list(to know when to shift by double)
                    usedLetters.Add(encrWord[i]);

                    encriptedWord += list[(list.IndexOf(encrWord[i]) + step) % list.Count];

                    //changing the shift back to normal for characters that only appeared once
                    step = stepFirst;
                }
                Console.WriteLine($"Your message is: {encriptedWord}");
                Console.WriteLine($"Your key: {string.Join(" ", encrKey)}");
            }

            else if (encrOpt == "decr")
            {
                String decriptedWord = "";
                Console.Write("Enter a word to be decripted: ");
                String? decrWord = Console.ReadLine().ToLower();
                Console.Write("Enter ur key separated with ' ': ");
                String? decrKeyInp = Console.ReadLine();

                List<int> decrKey = decrKeyInp.Split(' ').Select(int.Parse).ToList();

                int keyValuesUsed = 1;

                int steps = decrKey[0];
                int firstStep = steps;
                int sameChars=1;

                for (int i = 0; i < decrWord.Length; i++)
                {
                    //if the char is not a letter
                    if (!list.Contains(decrWord[i]))
                    {
                        decriptedWord += decrWord[i];
                        continue;
                    }

                    //decoding the extra safety pattern
                    if ((firstStep+1)%sameChars==0)
                    {
                        sameChars=1;
                    }     

                    //if i==index of a letter that appeared twice when encripting then *2
                    if (keyValuesUsed < decrKey.Count && i == decrKey[keyValuesUsed])
                    {
                        for (int k = 0; k < sameChars; k++)
                        {
                            steps += firstStep;
                        }

                        //increasing the num of letters that appeered more than once that were used up
                        if (keyValuesUsed < decrKey.Count - 1)
                        {
                            keyValuesUsed++;
                            sameChars++;
                        }
                    }

                    //decreasing by 26 to make sure it doesnt go out of bounds of the list
                    while (steps >= list.Count)
                    {
                        steps -= list.Count;
                    }

                    decriptedWord += list[(list.IndexOf(decrWord[i]) - steps + list.Count) % list.Count];
                    
                    //changing the shift back to normal for letters that appeared only once yet
                    steps = firstStep;
                }
                Console.WriteLine(decriptedWord);
            }
        }
    }
}
