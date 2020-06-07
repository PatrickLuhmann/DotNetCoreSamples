using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleSamples.Casino_Sample
{
	public class CasinoSample : IConsoleSample
	{
		public void Run()
		{
			Console.WriteLine("Welcome to the Casino Sample.");
			Console.WriteLine();

			bool quit = false;
			do
			{
				Console.WriteLine("Which fun activity would you like to do?");
				Console.WriteLine("1. Simulate poker hand distributions");
				Console.WriteLine("Q. Quit this sample");
				Console.WriteLine("> ");
				string userInput = Console.ReadLine();
				switch (userInput.ToUpper())
				{
					case "1":
						PokerHandDistributions();
						break;
					case "Q":
						quit = true;
						break;
					default:
						Console.WriteLine("ERROR: Invalid option!");
						break;
				}
			} while (!quit);
		}

		public void PokerHandDistributions()
		{
			int numIterations = 100000;
			bool printHands = false;
			bool quit = false;
			do
			{
				string simulation = null;
				Console.WriteLine();
				Console.WriteLine("Which simulation would you like to run?");
				Console.WriteLine("1. Simple 5 random cards");
				Console.WriteLine("2. Texas Hold 'em");
				Console.WriteLine("3. 7-Card Mashup");
				Console.WriteLine($"N. Set number of iterations [{numIterations} @ ~ {numIterations / 50000} secs]");
				Console.WriteLine($"P. Toggle print of each hand [{printHands}]");
				Console.WriteLine("Q. Quit this menu");
				string userInput = Console.ReadLine();
				switch (userInput.ToUpper())
				{
					case "1":
						simulation = "5 cards";
						break;
					case "2":
						simulation = "texas";
						break;
					case "3":
						simulation = "7 card mashup";
						break;
					case "N":
						Console.WriteLine("Enter the number of iterations: ");
						userInput = Console.ReadLine();
						numIterations = Convert.ToInt32(userInput);
						break;
					case "P":
						printHands = !printHands;
						break;
					case "Q":
						quit = true;
						break;
					default:
						Console.WriteLine("ERROR: Invalid option!");
						break;
				}

				if (simulation == "5 cards")
				{
					// Simple 5-card simulation.
					Deck deckOfCards = new Deck();
					//int[] Results = new int[10]; // index 9 is for chopped pots.
					int[] HandRanks = new int[9]; // just the hand, not compared to another.
					for (int iter = 0; iter < numIterations; iter++)
					{
						deckOfCards.Shuffle();
						Card[] hand = new Card[5];
						//Card[] handPlayer2 = new Card[5];
						hand[0] = deckOfCards.Deal();
						hand[1] = deckOfCards.Deal();
						hand[2] = deckOfCards.Deal();
						hand[3] = deckOfCards.Deal();
						hand[4] = deckOfCards.Deal();
						//handPlayer2[0] = deckOfCards.Deal();
						//handPlayer2[1] = deckOfCards.Deal();
						//handPlayer2[2] = deckOfCards.Deal();
						//handPlayer2[3] = deckOfCards.Deal();
						//handPlayer2[4] = deckOfCards.Deal();
						int valudOfHand = PokerGame.EvaluateHand(hand);
						HandRanks[valudOfHand >> 20]++;
						//int valuePlayer2 = PokerGame.EvaluateHand(handPlayer2);
						//HandRanks[valuePlayer2 >> 20]++;
#if false
						if (valudOfHand > valuePlayer2)
						{
							Results[valudOfHand >> 20]++;
						}
						else if (valuePlayer2 > valudOfHand)
						{
							Results[valuePlayer2 >> 20]++;
						}
						else
						{
							Results[9]++;
						}
#endif

						if (printHands)
						{
							Console.Write("The hand that has been dealt: ");
							foreach (Card card in hand)
							{
								Console.Write($"{card.Rank}{card.Suit} ");
							}
							Console.WriteLine($"which has a value of 0x{valudOfHand:X8}");
#if false
							Console.Write("Player 2 has been dealt: ");
							foreach (Card card in handPlayer2)
							{
								Console.Write($"{card.Rank}{card.Suit} ");
							}
							Console.WriteLine($"which has a value of 0x{valuePlayer2:X8}");
							if (valudOfHand > valuePlayer2)
							{
								Console.WriteLine("Player 1 wins!");
							}
							else if (valuePlayer2 > valudOfHand)
							{
								Console.WriteLine("Player 2 wins!");
							}
							else
							{
								Console.WriteLine("EVERYONE LOVES A CHOP POT!!!");
							}
#endif
						}
						else if (iter % 1000 == 0)
							Console.Write(".");

						// Don't forget to return the cards to the deck when the hand is over!
						deckOfCards.Return(hand);
						//deckOfCards.Return(handPlayer2);
					}
					decimal percent;
					string[] strenghtNames = {
						"High Card",
						"One Pair",
						"Two Pair",
						"Three Of A Kind",
						"Straight",
						"Flush",
						"Full House",
						"Four Of A Kind",
						"Straight Flush",
						"Tie"};
					Console.WriteLine("Simulation Results");
					Console.WriteLine("==================");

					Console.WriteLine("Hand Ranks");
					Console.WriteLine("----------");
					for (int idx = 0; idx < HandRanks.Length; idx++)
					{
						percent = Decimal.Divide(HandRanks[idx], numIterations);
						Console.WriteLine("{0,-16} [{1,6:P1}] {2,10}",
							strenghtNames[idx], percent, HandRanks[idx]);
					}
					Console.WriteLine();

#if false
					Console.WriteLine("Winning Hands");
					Console.WriteLine("-------------");
					for (int idx = 0; idx < Results.Length; idx++)
					{
						percent = Decimal.Divide(Results[idx], numIterations);
						Console.WriteLine("{0,-16} [{1,6:P1}] {2,10}",
							strenghtNames[idx], percent, Results[idx]);
					}
					Console.WriteLine();
#endif
				}

				if (simulation == "texas")
				{
					// Texas Hold 'em simulation.
					Deck deckOfCards = new Deck();
					int[] HandRanks = new int[9]; // just the hand, not compared to another.
					for (int iter = 0; iter < numIterations; iter++)
					{
						// Create the hand.
						deckOfCards.Shuffle();
						List<Card> hand = new List<Card>();
						hand.Add(deckOfCards.Deal());
						hand.Add(deckOfCards.Deal());
						hand.Add(deckOfCards.Deal());
						hand.Add(deckOfCards.Deal());
						hand.Add(deckOfCards.Deal());
						hand.Add(deckOfCards.Deal());
						hand.Add(deckOfCards.Deal());

						// Get the strength of the best 5-card hand.
						int valueOfHand = PokerGame.GetBest_5From7_All21(hand);

						// Increment the count for this ranking of hand.
						HandRanks[valueOfHand >> 20]++;

						if (printHands)
						{
							Console.Write("The hand that has been dealt: ");
							foreach (Card card in hand)
							{
								Console.Write($"{card.Rank}{card.Suit} ");
							}
							Console.WriteLine($"which has a value of 0x{valueOfHand:X8}");
						}
						else if (iter % 1000 == 0)
							Console.Write(".");

						// Don't forget to return the cards to the deck when the hand is over!
						deckOfCards.Return(hand);
					}
					decimal percent;
					string[] strenghtNames = {
						"High Card",
						"One Pair",
						"Two Pair",
						"Three Of A Kind",
						"Straight",
						"Flush",
						"Full House",
						"Four Of A Kind",
						"Straight Flush",
						"Tie"};
					Console.WriteLine();
					Console.WriteLine("Simulation Results");
					Console.WriteLine("==================");

					Console.WriteLine("Hand Ranks");
					Console.WriteLine("----------");
					for (int idx = 0; idx < HandRanks.Length; idx++)
					{
						percent = Decimal.Divide(HandRanks[idx], numIterations);
						Console.WriteLine("{0,-16} [{1,6:P1}] {2,10}",
							strenghtNames[idx], percent, HandRanks[idx]);
					}
					Console.WriteLine();
				}

				if (simulation == "7 card mashup")
				{
					// All 7 cards are played simulation.
					Deck deckOfCards = new Deck();
					Dictionary<string, int> HandRanks = new Dictionary<string, int>();
					HandRanks.Add("Flush7", 0);
					HandRanks.Add("Flush6", 0);
					HandRanks.Add("Flush5", 0);
					HandRanks.Add("Straight7", 0);
					HandRanks.Add("Straight6", 0);
					HandRanks.Add("Straight5", 0);

					// POC method; compare to existing method.
					Dictionary<string, int> HandRanks2 = new Dictionary<string, int>();
					HandRanks2.Add("StraightFlush7", 0);
					HandRanks2.Add("StraightFlush6", 0);
					HandRanks2.Add("StraightFlush5Pair", 0);
					HandRanks2.Add("StraightFlush5", 0);
					HandRanks2.Add("Flush7", 0);
					HandRanks2.Add("Flush6", 0);
					HandRanks2.Add("Flush5Pair", 0);
					HandRanks2.Add("Flush5", 0);
					HandRanks2.Add("Straight7", 0);
					HandRanks2.Add("Straight6", 0);
					HandRanks2.Add("Straight5Pair", 0);
					HandRanks2.Add("Straight5", 0);
					HandRanks2.Add("QuadSet", 0);
					HandRanks2.Add("QuadPair", 0);
					HandRanks2.Add("Quad", 0);
					HandRanks2.Add("SetSet", 0);
					HandRanks2.Add("SetPairPair", 0);
					HandRanks2.Add("SetPair", 0);
					HandRanks2.Add("Set", 0);
					HandRanks2.Add("PairPairPair", 0);
					HandRanks2.Add("PairPair", 0);
					HandRanks2.Add("Pair", 0);
					HandRanks2.Add("HighCard", 0);

					List<string> pocResults;
					for (int iter = 0; iter < numIterations; iter++)
					{
						// Create the hand.
						deckOfCards.Shuffle();
						List<Card> hand = new List<Card>();
						hand.Add(deckOfCards.Deal());
						hand.Add(deckOfCards.Deal());
						hand.Add(deckOfCards.Deal());
						hand.Add(deckOfCards.Deal());
						hand.Add(deckOfCards.Deal());
						hand.Add(deckOfCards.Deal());
						hand.Add(deckOfCards.Deal());
						hand.Sort();
						hand.Reverse();

#if false
						// not sure how to evaluate this yet, so start small.
						int flushLen = PokerGame.LargestFlush(hand);
						if (flushLen == 7)
							HandRanks["Flush7"]++;
						else if (flushLen == 6)
							HandRanks["Flush6"]++;
						else if (flushLen == 5)
							HandRanks["Flush5"]++;

						int straightLen = PokerGame.LargestStraight(hand);
						if (straightLen == 7)
							HandRanks["Straight7"]++;
						else if (straightLen == 6)
							HandRanks["Straight6"]++;
						else if (straightLen == 5)
							HandRanks["Straight5"]++;
#endif
						// POC new method
						pocResults = PokerGame.PocNewMethodFor7Cards(hand);
						foreach (string res in pocResults)
							HandRanks2[res]++;

						if (printHands)
						{
							foreach (Card card in hand)
							{
								Console.Write($"{card.Rank}{card.Suit} ");
							}
							//Console.Write($"{straightLen}_str8 ");
							//Console.Write($"{flushLen}_flush ");
							foreach (string res in pocResults)
								Console.Write($"{res} ");
							Console.WriteLine();
						}
						else if (iter % 50000 == 0)
							Console.Write(".");

						// Don't forget to return the cards to the deck when the hand is over!
						deckOfCards.Return(hand);
					}
					Console.WriteLine();
					Console.WriteLine("Simulation Results");
					Console.WriteLine("==================");

					Console.WriteLine("Hand Ranks");
					Console.WriteLine("----------");
					decimal percent;
#if false
					foreach (var key in HandRanks)
					{
						percent = Decimal.Divide(key.Value, numIterations);
						Console.WriteLine($"{key.Key} [{percent:P1}] {key.Value}");
					}
					Console.WriteLine();
#endif
					Console.WriteLine("Using the new method");
					var ordered = HandRanks2.OrderBy(x => x.Value);
					foreach (var key in ordered)
					{
						percent = Decimal.Divide(key.Value, numIterations);
						Console.WriteLine($"{key.Key,25} [{percent,5:P1}] {key.Value}");
					}
					Console.WriteLine();
				}
			} while (!quit);
		}

	}
}
