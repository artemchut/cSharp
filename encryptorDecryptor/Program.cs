using System;
using System.Collections.Generic;

namespace encrypter
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            List<char> list = new List<char> { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p',
                                               'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            Console.Write("To encr/decr?: ");
            String? encrOpt = Console.ReadLine().ToLower();

            int step = random.Next(1, 26);

            if (encrOpt == "encr")
            {
                String encriptedWord = "";
                Console.Write("Enter a word to be encripted: ");
                String? encrWord = Console.ReadLine().ToLower();
                for (int i = 0; i < encrWord.Length; i++)
                {
                    if (!list.Contains(encrWord[i]))
                    {
                        encriptedWord += encrWord[i];
                        continue;
                    }
                    if (list.IndexOf(encrWord[i]) + step > list.Count)
                    {
                        encriptedWord += list[Math.Abs(list.IndexOf(encrWord[i]) + step - list.Count)];
                    }
                    else if(list.IndexOf(encrWord[i]) + step < list.Count)
                    {
                        encriptedWord += list[list.IndexOf(encrWord[i]) + step];
                    }
                }
                Console.WriteLine($"It was shifted by {step}.");
                Console.WriteLine(encriptedWord);
            }

            else if (encrOpt == "decr")
            {
                String decriptedWord = "";
                Console.Write("Enter a word to be decripted: ");
                String? decrWord = Console.ReadLine().ToLower();
                Console.Write("Enter by how many it was shifted: ");
                int shiftedBy = Convert.ToInt32(Console.ReadLine());
                for (int i = 0; i < decrWord.Length; i++)
                {
                    if (!list.Contains(decrWord[i]))
                    {
                        decriptedWord += decrWord[i];
                        continue;
                    }
                    if (list.IndexOf(decrWord[i]) - shiftedBy < 0)
                    {
                        decriptedWord += list[Math.Abs(list.IndexOf(decrWord[i]) - shiftedBy + list.Count)];

                    }
                    else
                    {
                        decriptedWord += list[list.IndexOf(decrWord[i]) - shiftedBy];
                    }
                }
                Console.WriteLine(decriptedWord);
            }
        }
    }
}