using Google.Apis.Docs.v1.Data;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleSamples.Casino_Sample
{
	public class CasinoSample : IConsoleSample
	{
		Dictionary<string, int> HandToIndex_7CardMash = new Dictionary<string, int>();
		// Certain hands can have multiple possible interpretations.
		// EX: Straight5 and Flush5.
		// EX: Flush6 and Flush5Pair.
		// EX: Straight5Pair, Straight6, Flush5.
		int[] NumHandInterpretations = new int[3];
		int[] HandRankCount;
		int NumResults = 0;

		public CasinoSample()
		{
			HandToIndex_7CardMash.TryAdd("StraightFlush7", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("StraightFlush6", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("StraightFlush5Pair", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("StraightFlush5", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("Flush7", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("Flush6", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("Flush5Pair", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("Flush5", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("Straight7", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("Straight6", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("Straight5Pair", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("Straight5", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("QuadSet", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("QuadPair", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("Quad", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("SetSet", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("SetPairPair", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("SetPair", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("Set", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("PairPairPair", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("PairPair", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("Pair", HandToIndex_7CardMash.Count);
			HandToIndex_7CardMash.TryAdd("HighCard", HandToIndex_7CardMash.Count);

			HandRankCount = new int[HandToIndex_7CardMash.Count];
		}

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
			int numThreads = 4;
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
				Console.WriteLine($"N. Set number of iterations per thread [{numIterations}]");
				Console.WriteLine($"T. Set number of threads [{numThreads}");
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
					case "T":
						Console.WriteLine("Enter the number of threads to use: ");
						userInput = Console.ReadLine();
						numThreads = Convert.ToInt32(userInput);
						if (numThreads < 1)
							numThreads = 1;
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

					Array.Clear(HandRankCount, 0, HandRankCount.Length);
					Array.Clear(NumHandInterpretations, 0, NumHandInterpretations.Length);
					NumResults = 0;

					Parallel.For(0, numThreads
						, iterator =>
					{
						Simulate7CardMash(numIterations, printHands, iterator);
					}); // end Parallel.For

					Console.WriteLine();
					Console.WriteLine("Simulation Results");
					Console.WriteLine("==================");

					Console.WriteLine("Hand Ranks");
					Console.WriteLine("----------");
					decimal percent;
					Console.WriteLine("Using the new method");
					Console.WriteLine($"Total number of results: {NumResults}");
					Console.WriteLine($"Number of hands with 2 possible interpretations: {NumHandInterpretations[1]}");
					Console.WriteLine($"Number of hands with 3 possible interpretations: {NumHandInterpretations[2]}");
					var ordered = HandToIndex_7CardMash.OrderBy(x => HandRankCount[x.Value]);
					foreach (var key in ordered)
					{
						percent = Decimal.Divide(HandRankCount[key.Value], NumResults);
						Console.WriteLine($"{key.Key,25} [{percent,6:P1}] {HandRankCount[key.Value]}");
					}
					Console.WriteLine();
				}
			} while (!quit);
		}

		private void Simulate7CardMash(int numIterations, bool printHands, int threadNum)
		{
			Deck deckOfCards = new Deck();
			List<Card> hand = new List<Card>();
			List<string> pocResults;
			int[] myHandRankCount = new int[HandToIndex_7CardMash.Count];
			int myNumResults = 0;
			int[] myNumHandInterpretations = new int[3];
			for (int iter = 0; iter < numIterations; iter++)
			{
				// Create the hand.
				deckOfCards.Shuffle();
				hand.Add(deckOfCards.Deal());
				hand.Add(deckOfCards.Deal());
				hand.Add(deckOfCards.Deal());
				hand.Add(deckOfCards.Deal());
				hand.Add(deckOfCards.Deal());
				hand.Add(deckOfCards.Deal());
				hand.Add(deckOfCards.Deal());
				hand.Sort();
				hand.Reverse();

				pocResults = PokerGame.PocNewMethodFor7Cards(hand);
				myNumHandInterpretations[pocResults.Count - 1]++;

				foreach (string res in pocResults)
				{
					myHandRankCount[HandToIndex_7CardMash[res]]++;
					myNumResults++;
				}

				if (printHands)
				{
					string handOut = "";
					foreach (Card card in hand)
					{
						handOut += $"{card.Rank}{card.Suit} ";
					}
					foreach (string res in pocResults)
						handOut += $"{res} ";
					Console.WriteLine($"{handOut}");
				}
				else if (iter % 50000 == 0)
					Console.Write($"{threadNum}");

				// Don't forget to return the cards to the deck when the hand is over!
				deckOfCards.Return(hand);
				hand.Clear();
			}

			// Update the various results accumulators.
			Interlocked.Add(ref NumResults, myNumResults);
			for (int i = 0; i < myHandRankCount.Length; i++)
			{
				Interlocked.Add(ref HandRankCount[i], myHandRankCount[i]);
			}
			for (int i = 0; i < NumHandInterpretations.Length; i++)
			{
				Interlocked.Add(ref NumHandInterpretations[i], myNumHandInterpretations[i]);
			}
		}
	}
}
