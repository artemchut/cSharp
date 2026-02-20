using System;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Threading.Tasks.Dataflow;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();

            Minions bored_silly = new Minions("bored_silly", 10, 3, 8, 25, 27);
            Minions uhh_caveman = new Minions("uhh_caveman", 12, 5, 55, 56, 32);
            Minions what_will_happen_next = new Minions("what_will_happen_next", 25, 8, 24, 70, 96);
            Minions meet_scarlet_overkill = new Minions("meet_scarlet_overkill", 27, 6, 25, 30, 90);
            Minions denim_dungarees = new Minions("denim_dungarees", 30, 2, 33, 64, 21);
            Minions the_journey_begins = new Minions("the_journey_begins", 15, 4, 40, 75, 45);
            Minions kevin = new Minions("kevin", 28, 10, 50, 42, 99);
            Minions with_the_most_evil = new Minions("with_the_most_evil", 28, 5, 32, 40, 47);
            Minions bob = new Minions("bob", 27, 8, 48, 50, 97);
            Minions stuart = new Minions("stuart", 25, 9, 49, 55, 98);
            Minions feeling_blue = new Minions("feeling_blue", 5, 6, 10, 20, 32);
            List<Minions> allCards = new List<Minions>
            {
                bored_silly, uhh_caveman, what_will_happen_next, meet_scarlet_overkill, denim_dungarees, the_journey_begins, kevin, with_the_most_evil, bob, stuart, feeling_blue
            };


            void userWon(List<Minions> userCards, List<Minions> computerCards)
            {
                Console.WriteLine();
                Console.WriteLine("You won this round");
                userCards.Add(userCards[0]);
                userCards.RemoveAt(0);
                userCards.Add(computerCards[0]);
                computerCards.RemoveAt(0);
            }
            void userLost(List<Minions> userCards, List<Minions> computerCards)
            {
                Console.WriteLine();
                Console.WriteLine("You lost this round");
                computerCards.Add(computerCards[0]);
                computerCards.RemoveAt(0);
                computerCards.Add(userCards[0]);
                userCards.RemoveAt(0);
            }


            // CREATING DECKS OF CARDS
            int deckSize = 5;

            // USER'S CARDS
            List<Minions> userCards = new List<Minions> { };

            List<int> shuffleIndexes = new List<int> { };
            for (int i = 0; i < deckSize; i++)
            {
                int shuffler = random.Next(0, allCards.Count());
                while (shuffleIndexes.Contains(shuffler))
                {
                    shuffler = random.Next(0, allCards.Count());
                }
                shuffleIndexes.Add(shuffler);
                userCards.Add(allCards[shuffler]);
            }

            // COMPUTER'S CARDS
            List<Minions> computerCards = new List<Minions> { };
            for (int i = 0; i < deckSize; i++)
            {
                Minions randomCard = allCards[random.Next(0, allCards.Count())];
                while (userCards.Contains(randomCard) || computerCards.Contains(randomCard))
                {
                    randomCard = allCards[random.Next(0, allCards.Count())];
                }
                computerCards.Insert(0, randomCard);
            }



            List<List<string>> allMeasurements = new List<List<string>>
            {
                new List<string> { "cl","le","ev","ve","er","rn","ne","es","ss" },
                new List<string> { "br","ra","av","ve","er","ry" },
                new List<string> { "le","ea","ad","de","er","rs","sh","hi","ip" },
                new List<string> { "mi","is","sc","ch","hi","ev","vo","ou","us","sn","ne","es","ss" },
                new List<string> { "ra", "at", "ti", "in", "ng" }
            };

            void MeasurementValidation(Minions currentUserCard, Minions currentComputerCard)
            {
                // PRINTING CARD'S STATS
                Console.WriteLine();
                Console.WriteLine("Here are the stats of your current card:");
                Console.WriteLine($"Cleverness: {currentUserCard.GetCleverness()}");
                Console.WriteLine($"Bravery: {currentUserCard.GetBravery()}");
                Console.WriteLine($"Leadership: {currentUserCard.GetLeadership()}");
                Console.WriteLine($"Mischievousness: {currentUserCard.GetMischievousness()}");
                Console.WriteLine($"Rating: {currentUserCard.GetTopThrumpsRating()}");


                Console.Write("Enter using what measure you want to fight with(eg. cleverness): ");
                string? measurement = Console.ReadLine().ToLower();

                // BREAKING THE MEASUREMENT INTO PAIRS OF 2
                List<string> brokenMeasurement = new List<string> { };
                for (int i = measurement.Length - 1; i >= 1; i--)
                {
                    brokenMeasurement.Insert(0, measurement.Substring(i - 1, 2));
                }

                // CHECKING WHICH ONE'S THE CLOSEST
                int numLexemes = 0;
                int highestNumLexemes = 0;
                int closest = 0;
                for (int i = 0; i < allMeasurements.Count(); i++)
                {
                    for (int k = 0; k < brokenMeasurement.Count(); k++)
                    {
                        try
                        {
                            if (brokenMeasurement[k] == allMeasurements[i][k])
                            {
                                numLexemes++;
                            }
                        }
                        catch
                        {
                            continue;
                        }
                        if (numLexemes > highestNumLexemes)
                        {
                            highestNumLexemes = numLexemes;
                            closest = i;
                        }
                    }
                }
                // CONVERTING TO % FORMAT
                double percentageSame = ((double)highestNumLexemes / allMeasurements[closest].Count()) * 100;
                if (percentageSame >= 20)
                {
                    if (closest == 0)
                    {
                        if (currentUserCard.GetCleverness() > currentComputerCard.GetCleverness())
                        {
                            userWon(userCards, computerCards);
                        }
                        else
                        {
                            userLost(userCards, computerCards);
                        }
                    }
                    else if (closest == 1)
                    {
                        if (currentUserCard.GetBravery() > currentComputerCard.GetBravery())
                        {
                            userWon(userCards, computerCards);
                        }
                        else
                        {
                            userLost(userCards, computerCards);
                        }
                    }
                    else if (closest == 2)
                    {
                        if (currentUserCard.GetLeadership() > currentComputerCard.GetLeadership())
                        {
                            userWon(userCards, computerCards);
                        }
                        else
                        {
                            userLost(userCards, computerCards);
                        }
                    }
                    else if (closest == 3)
                    {
                        if (currentUserCard.GetMischievousness() > currentComputerCard.GetMischievousness())
                        {
                            userWon(userCards, computerCards);
                        }
                        else
                        {
                            userLost(userCards, computerCards);
                        }
                    }
                    else if (closest == 4)
                    {
                        if (currentUserCard.GetTopThrumpsRating() > currentComputerCard.GetTopThrumpsRating())
                        {
                            userWon(userCards, computerCards);
                        }
                        else
                        {
                            userLost(userCards, computerCards);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Please try again, pick one from shown below:");
                    MeasurementValidation(currentUserCard, currentComputerCard);
                }
            }

            bool won = false;
            while (!won)
            {
                Minions currentUserCard = userCards[0];
                Minions currentComputerCard = computerCards[0];

                // GETTING AN INPUT FROM A USER (cleverness/rating/...)
                MeasurementValidation(currentUserCard, currentComputerCard);

                Console.WriteLine($"U got {userCards.Count()} cards now");
                Console.WriteLine($"Computer got {computerCards.Count()} cards now");

                if (userCards.Count() == 0)
                {
                    Console.WriteLine("---------");
                    Console.WriteLine("You lost this battle");
                    won = true;
                }
                else if (computerCards.Count() == 0)
                {
                    Console.WriteLine("---------------");
                    Console.WriteLine("You won this battle!!");
                    won = true;
                }
            }
        }


        // CARDS CLASS
        public class Minions
        {
            private string cardName;
            private int cleverness;
            private int bravery;
            private int leadership;
            private int mischievousness;
            private int topThrupsRating;


            public Minions(string minionCardName, int minionCleverness, int minionBravery, int minionLeadership, int minionMischievousness, int minionThrumpsRating)
            {
                cardName = minionCardName;
                cleverness = minionCleverness;
                bravery = minionBravery;
                leadership = minionLeadership;
                mischievousness = minionMischievousness;
                topThrupsRating = minionThrumpsRating;
            }

            public string GetCardName()
            {
                return cardName;
            }
            public int GetCleverness()
            {
                return cleverness;
            }
            public int GetBravery()
            {
                return bravery;
            }
            public int GetLeadership()
            {
                return leadership;
            }
            public int GetMischievousness()
            {
                return mischievousness;
            }
            public int GetTopThrumpsRating()
            {
                return topThrupsRating;
            }
        }
    }
}
