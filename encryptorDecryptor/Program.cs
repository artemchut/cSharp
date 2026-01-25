using System;
using System.Collections.Generic;
using Microsoft.VisualBasic;
using System.Linq;

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

            if (encrOpt == "encr")
            {
                int stepFirst = random.Next(1, 26);
                while (stepFirst == 13)
                {
                    stepFirst = random.Next(1, 26);
                }
                int step = stepFirst;
                String encryptedWord = "";
                Console.Write("Enter a word to be encripted: ");
                String? encrWord = Console.ReadLine().ToLower();
                encrKey.Add(stepFirst.ToString());

                for (int i = 0; i < encrWord.Length; i++)
                {
                    //if char is not a letter
                    if (!list.Contains(encrWord[i]))
                    {
                        encryptedWord += encrWord[i];
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
                    if (i==encrWord.Length/2)
                    {
                        int stepFirst2 = random.Next(1, 26);
                        while (stepFirst == 13 || stepFirst2==stepFirst)
                        {
                            stepFirst = random.Next(1, 26);
                        }
                        step = stepFirst;
                        sameLetters=0;
                        usedLetters.Clear();
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

                    encryptedWord += list[(list.IndexOf(encrWord[i]) + step) % list.Count];

                    //changing the shift back to normal for characters that only appeared once
                    step = stepFirst;
                }
                Console.WriteLine($"Your message is: {encryptedWord}");
                Console.WriteLine($"Your key: {string.Join(" ", encrKey)} {stepFirst}");
            }

            else if (encrOpt == "decr")
            {
                String decryptedWord = "";
                Console.Write("Enter a word to be decripted: ");
                String? decrWord = Console.ReadLine().ToLower();
                Console.Write("Enter ur key separated with ' ': ");
                String? decrKeyInp = Console.ReadLine();

                List<int> decrKey = decrKeyInp.Split(' ').Select(int.Parse).ToList();
                //first value is a key
                int keyValuesUsed = 1;

                int steps = decrKey[0];
                int firstStep = steps;
                int sameChars=1;

                for (int i = 0; i < decrWord.Length; i++)
                {
                    //if the char is not a letter
                    if (!list.Contains(decrWord[i]))
                    {
                        decryptedWord += decrWord[i];
                        continue;
                    }

                    //decoding the extra safety pattern
                    if (firstStep/sameChars==0)
                    {
                        sameChars=1;
                    }      
                    //if index is the middle-then switch to a new random shift
                    if (i == decrWord.Length / 2)
                    {
                        firstStep = decrKey[^1];
                        steps = firstStep;
                        sameChars = 1;
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

                    decryptedWord += list[(list.IndexOf(decrWord[i]) - steps + list.Count) % list.Count];
                    
                    //changing the shift back to normal for letters that appeared only once yet
                    steps = firstStep;
                }
                Console.WriteLine(decryptedWord);
            }
        }
    }
}
