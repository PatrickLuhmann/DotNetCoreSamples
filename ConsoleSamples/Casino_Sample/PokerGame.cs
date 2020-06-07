using Google.Apis.Docs.v1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleSamples.Casino_Sample
{
	public class PokerGame
	{
		/// <summary>
		/// Returns the strength of the given hand.
		/// </summary>
		/// <param name="Hand"></param>
		/// <returns>An int representing the strength of the hand.</returns>
		/// Hand Ranking Encoding:
		/// straight flush [highest card]
		/// [+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+]
		/// [+   reserved    +   8   +  RH   +  RMH  +  RM   +  RML  +  RL   +]
		/// four of a kind [common rank][kicker]
		/// [+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+]
		/// [+   reserved    +   7   +  QV   +  QV   +  QV   +  QV   +  RH   +]
		/// full house [three-card rank][two-card rank]
		/// [+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+]
		/// [+   reserved    +   6   +  TV   +  TV   +  TV   +  PV   +  PV   +]
		/// flush [rank of each card from highest to lowest]
		/// [+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+]
		/// [+   reserved    +   5   +  RH   +  RMH  +  RM   +  RML  +  RL   +]
		/// straight [highest card]
		/// [+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+]
		/// [+   reserved    +   4   +  RH   +  RMH  +  RM   +  RML  +  RL   +]
		/// three of a kind [common rank][higher kicker][lower kicker]
		/// [+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+]
		/// [+   reserved    +   3   +  TV   +  TV   +  TV   +  RH   +  RL   +]
		/// two pair [higher pair rank][lower pair rank][kicker]
		/// [+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+]
		/// [+   reserved    +   2   +  PH   +  PH   +  PL   +  PL   +  RH   +]
		/// one pair [pair rank][highest kicker][middle kicker][lowest kicker]
		/// [+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+]
		/// [+   reserved    +   1   +  PV   +  PV   +  RH   +  RM   +  RL   +]
		/// high card [rank of each card from highest to lowest]
		/// [+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+]
		/// [+   reserved    +   0   +  RH   +  RMH  +  RM   +  RML  +  RL   +]
		public static int EvaluateHand(Card[] Hand)
		{
			const int StraightFlush = 0x00800000;
			const int FourOfAKind = 0x00700000;
			const int FullHouse = 0x00600000;
			const int Flush = 0x00500000;
			const int Straight = 0x00400000;
			const int ThreeOfAKind = 0x00300000;
			const int TwoPair = 0x00200000;
			const int OnePair = 0x00100000;
			const int HighCard = 0x00000000;
			int strength = -1;

			// Check for flush
			bool flush = false;
			if (Hand[0].Suit == Hand[1].Suit &&
				Hand[0].Suit == Hand[2].Suit &&
				Hand[0].Suit == Hand[3].Suit &&
				Hand[0].Suit == Hand[4].Suit)
				flush = true;

			// Check for straight
			bool straight = false;
			bool wheel = false;
			// NOTE: This sorts by rank from low to high, even though
			// we think of poker hands as going from high to low.
			Array.Sort(Hand);
			if (Hand[0].RankValue == Hand[1].RankValue - 1 &&
				Hand[1].RankValue == Hand[2].RankValue - 1 &&
				Hand[2].RankValue == Hand[3].RankValue - 1)
			{
				// Normal case
				if (Hand[3].RankValue == Hand[4].RankValue - 1)
					straight = true;
				// Ace-low (i.e. the wheel) case
				if (Hand[0].RankValue == 2 && Hand[4].RankValue == 14)
				{
					straight = true;
					wheel = true;
				}
			}

			// Are we a straight flush?
			if (flush && straight)
			{
				strength = StraightFlush;
				if (wheel)
				{
					strength |= (Hand[3].RankValue << 16);
					strength |= (Hand[2].RankValue << 12);
					strength |= (Hand[1].RankValue << 8);
					strength |= (Hand[0].RankValue << 4);
					strength |= (1 << 0); // Ace == 1 when it is low
				}
				else
				{
					strength |= (Hand[4].RankValue << 16);
					strength |= (Hand[3].RankValue << 12);
					strength |= (Hand[2].RankValue << 8);
					strength |= (Hand[1].RankValue << 4);
					strength |= (Hand[0].RankValue << 0);
				}

				return strength;
			}

			// Are we a flush?
			if (flush)
			{
				strength = Flush;
				strength |= (Hand[4].RankValue << 16);
				strength |= (Hand[3].RankValue << 12);
				strength |= (Hand[2].RankValue << 8);
				strength |= (Hand[1].RankValue << 4);
				strength |= (Hand[0].RankValue << 0);

				return strength;
			}

			// Are we a straight?
			if (straight)
			{
				strength = Straight;
				if (wheel)
				{
					strength |= (Hand[3].RankValue << 16);
					strength |= (Hand[2].RankValue << 12);
					strength |= (Hand[1].RankValue << 8);
					strength |= (Hand[0].RankValue << 4);
					strength |= (1 << 0); // Ace == 1 when it is low
				}
				else
				{
					strength |= (Hand[4].RankValue << 16);
					strength |= (Hand[3].RankValue << 12);
					strength |= (Hand[2].RankValue << 8);
					strength |= (Hand[1].RankValue << 4);
					strength |= (Hand[0].RankValue << 0);
				}

				return strength;
			}

			// Count matches to see if we have 2/3/4-of-a-kind.
			// Only look forward.
			// # Matches  Hand
			// ===============
			// 6  - Quads
			// 4  - Boat
			// 3  - Trips
			// 2  - 2 Pair
			// 1  - Pair
			// 0  - High Card
			int numMatches = 0;
			for (int i = 0; i < 4; i++)
			{
				for (int j = i + 1; j < 5; j++)
				{
					if (Hand[i].RankValue == Hand[j].RankValue)
						numMatches++;
				}
			}

			// Are we four-of-a-kind?
			if (numMatches == 6)
			{
				strength = FourOfAKind;
				if (Hand[0].RankValue == Hand[1].RankValue)
				{
					// e.g. 22223
					strength |= (Hand[0].RankValue << 16);
					strength |= (Hand[0].RankValue << 12);
					strength |= (Hand[0].RankValue << 8);
					strength |= (Hand[0].RankValue << 4);
					strength |= (Hand[4].RankValue << 0);
				}
				else
				{
					// e.g. 23333
					strength |= (Hand[1].RankValue << 16);
					strength |= (Hand[1].RankValue << 12);
					strength |= (Hand[1].RankValue << 8);
					strength |= (Hand[1].RankValue << 4);
					strength |= (Hand[0].RankValue << 0);
				}
			}
			// Are we a full house?
			else if (numMatches == 4)
			{
				strength = FullHouse;

				// Once sorted, the third card gives the bigger side.
				if (Hand[2].RankValue == Hand[1].RankValue)
				{
					strength |= Hand[0].RankValue << 16;
					strength |= Hand[1].RankValue << 12;
					strength |= Hand[2].RankValue << 8;
					strength |= Hand[3].RankValue << 4;
					strength |= Hand[4].RankValue << 0;
				}
				else
				{
					strength |= Hand[2].RankValue << 16;
					strength |= Hand[3].RankValue << 12;
					strength |= Hand[4].RankValue << 8;
					strength |= Hand[0].RankValue << 4;
					strength |= Hand[1].RankValue << 0;
				}
			}
			// Are we three of a kind?
			else if (numMatches == 3)
			{
				strength = ThreeOfAKind;

				// Once sorted, there are three possibiliies:
				// XXXyz, yXXXz, yzXXX.
				// Therefore, the third card is always a part of
				// the trips.
				int tripRank = Hand[2].RankValue;
				if (Hand[0].RankValue == tripRank)
				{
					strength |= tripRank << 16;
					strength |= tripRank << 12;
					strength |= tripRank << 8;
					strength |= Hand[4].RankValue << 4;
					strength |= Hand[3].RankValue << 0;
				}
				else if (Hand[4].RankValue == tripRank)
				{
					strength |= tripRank << 16;
					strength |= tripRank << 12;
					strength |= tripRank << 8;
					strength |= Hand[1].RankValue << 4;
					strength |= Hand[0].RankValue << 0;
				}
				else
				{
					strength |= tripRank << 16;
					strength |= tripRank << 12;
					strength |= tripRank << 8;
					strength |= Hand[4].RankValue << 4;
					strength |= Hand[0].RankValue << 0;
				}
			}
			// Are we two pair?
			else if (numMatches == 2)
			{
				strength = TwoPair;

				// Once sorted, there are three possibiliies:
				// zLLHH, LLHHz, LLzHH.
				if (Hand[0].RankValue != Hand[1].RankValue)
				{
					strength |= Hand[3].RankValue << 16;
					strength |= Hand[4].RankValue << 12;
					strength |= Hand[1].RankValue << 8;
					strength |= Hand[2].RankValue << 4;
					strength |= Hand[0].RankValue << 0;
				}
				else if (Hand[4].RankValue != Hand[3].RankValue)
				{
					strength |= Hand[2].RankValue << 16;
					strength |= Hand[3].RankValue << 12;
					strength |= Hand[0].RankValue << 8;
					strength |= Hand[1].RankValue << 4;
					strength |= Hand[4].RankValue << 0;
				}
				else
				{
					strength |= Hand[3].RankValue << 16;
					strength |= Hand[4].RankValue << 12;
					strength |= Hand[0].RankValue << 8;
					strength |= Hand[1].RankValue << 4;
					strength |= Hand[2].RankValue << 0;
				}
			}
			// Are we one pair?
			else if (numMatches == 1)
			{
				strength = OnePair;

				// Once sorted, there are four possibiliies:
				// PPxyz, xPPyz, xyPPz, xyzPP
				if (Hand[0].RankValue == Hand[1].RankValue)
				{
					strength |= Hand[0].RankValue << 16;
					strength |= Hand[1].RankValue << 12;
					strength |= Hand[4].RankValue << 8;
					strength |= Hand[3].RankValue << 4;
					strength |= Hand[2].RankValue << 0;
				}
				else if (Hand[1].RankValue == Hand[2].RankValue)
				{
					strength |= Hand[1].RankValue << 16;
					strength |= Hand[2].RankValue << 12;
					strength |= Hand[4].RankValue << 8;
					strength |= Hand[3].RankValue << 4;
					strength |= Hand[0].RankValue << 0;
				}
				else if (Hand[2].RankValue == Hand[3].RankValue)
				{
					strength |= Hand[2].RankValue << 16;
					strength |= Hand[3].RankValue << 12;
					strength |= Hand[4].RankValue << 8;
					strength |= Hand[1].RankValue << 4;
					strength |= Hand[0].RankValue << 0;
				}
				else
				{
					strength |= Hand[3].RankValue << 16;
					strength |= Hand[4].RankValue << 12;
					strength |= Hand[2].RankValue << 8;
					strength |= Hand[1].RankValue << 4;
					strength |= Hand[0].RankValue << 0;
				}
			}
			// Are we high card?
			else if (numMatches == 0)
			{
				// The cards are already sorted for us, we just need
				// to reverse the order to match what the caller expects.
				strength = HighCard;
				strength |= Hand[4].RankValue << 16;
				strength |= Hand[3].RankValue << 12;
				strength |= Hand[2].RankValue << 8;
				strength |= Hand[1].RankValue << 4;
				strength |= Hand[0].RankValue << 0;
			}
			else
			{
				// TODO: What type of exception should be thrown here?
				throw new Exception();
			}

			return strength;
		}

		static public int GetBest_5From7_All21(List<Card> cards)
		{
			int strength = -1;

			// Create all possible 5-card hands from the 7 we have to work with.
			List<List<Card>> allCombos = GetCombos(cards, 5);

			// Calculate the strength of each combo.
			foreach (List<Card> combo in allCombos)
			{
				int comboStr = EvaluateHand(combo.ToArray());
				if (comboStr > strength)
				{
					strength = comboStr;
				}
			}

			return strength;
		}
		static public int GetNumDistinctRanks(List<Card> cards)
		{
			int num = 0;
			int[] rankQty = new int[15]; // 0 and 1 are not used, but so what?
			foreach (Card card in cards)
			{
				rankQty[card.RankValue]++;
			}
			foreach (int val in rankQty)
				if (val > 0)
					num++;

			return num;
		}

		static public bool IsStraight(List<Card> cards)
		{
			if (cards == null || cards.Count == 0)
				return false;
			if (cards.Count == 1)
				return true;

			bool temp = true;
			for (int idx = 0; idx < cards.Count - 1; idx++)
				if (cards[idx].RankValue != cards[idx + 1].RankValue + 1)
					temp = false;

			if (temp)
				return true;

			// Special Case: The Wheel may still be possible.
			if (cards[0].Rank == "A")
			{
				List<Card> subset = cards.GetRange(1, cards.Count - 1);
				if (cards.Last().Rank == "2" && IsStraight(subset))
					return true;
			}

			return false;
		}

		static public List<List<Card>> GetCombos(List<Card> input, int n)
		{
			List<List<Card>> combos = new List<List<Card>>();

			if (n == 1)
			{
				foreach (Card card in input)
				{
					List<Card> newCombo = new List<Card>();
					newCombo.Add(card);
					combos.Add(newCombo);
				}
			}
			else
			{
				for (int i = 0; i < input.Count - 1; i++)
				{
					List<Card> tail = input.GetRange(i + 1, input.Count - i - 1);
					List<List<Card>> subCombos = GetCombos(tail, n - 1);
					foreach (List<Card> combo in subCombos)
					{
						List<Card> newCombo = new List<Card>();
						newCombo.Add(input[i]);
						foreach (Card card in combo)
							newCombo.Add(card);
						//string combo = input[i] + combo;
						combos.Add(newCombo);
					}
				}
			}

			return combos;
		}

		/// <summary>
		/// (Assumes cards are sorted from Ace to 2).
		/// </summary>
		/// <param name="cards">The set of cards to examine.</param>
		/// <returns></returns>
		static public int LargestFlush(List<Card> cards)
		{
			Dictionary<string, int> suitQty = new Dictionary<string, int>();
			suitQty.Add("c", 0);
			suitQty.Add("d", 0);
			suitQty.Add("h", 0);
			suitQty.Add("s", 0);
			foreach (Card card in cards)
			{
				suitQty[card.Suit]++;
			}
			int longest = 0;
			foreach (var item in suitQty)
				if (item.Value > longest)
					longest = item.Value;

			return longest;
		}

		/// <summary>
		/// (Assumes cards are sorted from Ace to 2).
		/// </summary>
		/// <param name="cards">The set of cards to examine.</param>
		/// <returns></returns>
		static public int LargestStraight(List<Card> cards)
		{
			int longest = 0;

			int[] ranks = new int[15]; // 0 is unused; too messy to do -1 everywhere
			foreach (Card card in cards)
				ranks[card.RankValue]++;
			// Aces can also be "1".
			ranks[1] = ranks[14];
			int run = 0;
			for (int i = 1; i < 15; i++)
			{
				if (ranks[i] > 0)
				{
					run++;
					if (run > longest)
						longest = run;
				}
				else
					run = 0;
			}
			return longest;
		}

		static public List<string> PocNewMethodFor7Cards(List<Card> cards)
		{
			List<string> results = new List<string>();

			// For flushes.
			Dictionary<string, int> suitQty = new Dictionary<string, int>
			{
				{ "c", 0 },
				{ "d", 0 },
				{ "h", 0 },
				{ "s", 0 }
			};
			List<Card> clubs = new List<Card>();
			List<Card> diamonds = new List<Card>();
			List<Card> hearts = new List<Card>();
			List<Card> spades = new List<Card>();
			// For straights and matches.
			int[] ranks = new int[15]; // 0 is unused; too messy to do -1 everywhere
									   // For matches.
			int numPair = 0;
			int numSet = 0;
			int numQuad = 0;

			// Scan through the cards looking for items of interest.
			foreach (Card card in cards)
			{
				// Suit count for flush.
				suitQty[card.Suit]++;
				switch (card.Suit)
				{
					case "c":
						clubs.Add(card);
						break;
					case "d":
						diamonds.Add(card);
						break;
					case "h":
						hearts.Add(card);
						break;
					case "s":
						spades.Add(card);
						break;
				}

				// Track ranks for both straights and matches.
				ranks[card.RankValue]++;
				// Check for new match.
				if (ranks[card.RankValue] == 2)
					numPair++;
				else if (ranks[card.RankValue] == 3)
				{
					numPair--;
					numSet++;
				}
				else if (ranks[card.RankValue] == 4)
				{
					numSet--;
					numQuad++;
				}
			}

			// Find the longest flush.
			int longestFlush = 0;
			string longestFlushSuit = "";
			foreach (var item in suitQty)
				if (item.Value > longestFlush)
				{
					longestFlush = item.Value;
					longestFlushSuit = item.Key;
				}

			// Find the longest straight.
			int longestStraight = 0;
			int runLen = 0;
			int runHighestRank = 0;
			ranks[1] = ranks[14];
			for (int i = 1; i < 15; i++)
			{
				if (ranks[i] > 0)
				{
					runLen++;
					if (runLen > longestStraight)
					{
						longestStraight = runLen;
						runHighestRank = i;
					}
				}
				else
					runLen = 0;
			}

			// There are two categories of value in a poker hand:
			// "run" (flush, straight) and "match" (pair, set, quad).
			// Check for a run first, because it is (or should be) rarer.
			if (longestFlush >= 5 || longestStraight >= 5)
			{
				// Start with flushes.
				// Within the flush category we will look for straight flushes as well.
				// This means we don't need to look for them in the straight category.
				if (longestFlush == 7)
				{
					// With all cards being the same suit, there can be no "matches",
					// so no need to look for them.
					if (longestStraight == 7)
						// StrFl7 is a superset of Fl7 so don't include it.
						results.Add("StraightFlush7");
					else
					{
						results.Add("Flush7");
						if (longestStraight == 6)
							results.Add("StraightFlush6");
						else if (longestStraight == 5)
							results.Add("StraightFlush5"); // can't be StraightFlush5Pair
					}
				}
				else if (longestFlush == 6)
				{
					results.Add("Flush6");
					// First look for straight flushes.
					int strFlushLen = 0;
					List<Card> flushCards = null;
					switch (longestFlushSuit)
					{
						case "c":
							strFlushLen = LargestStraight(clubs);
							flushCards = clubs;
							break;
						case "d":
							strFlushLen = LargestStraight(diamonds);
							flushCards = diamonds;
							break;
						case "h":
							strFlushLen = LargestStraight(hearts);
							flushCards = hearts;
							break;
						case "s":
							strFlushLen = LargestStraight(spades);
							flushCards = spades;
							break;
					}
					List<Card> notFlushCards = cards.Except(flushCards).ToList();
					if (strFlushLen == 6)
					{
						// StrFl6 is a superset of Flush6, so remove it.
						results.Remove("Flush6");
						results.Add("StraightFlush6");

						// We have a StrFl6, but do we also have a StrFl5 + Pair?
						// Only the first and last cards of the StrFl are eligible for
						// pairing, because only they can be removed and still maintain
						// the StrFl5.

						// Check for The Wheel.
						if (flushCards[0].RankValue == 14 && flushCards[1].RankValue != 13)
						{
							// The Wheel case.
							if (flushCards[0].RankValue == notFlushCards[0].RankValue ||
								flushCards[1].RankValue == notFlushCards[0].RankValue)
							{
								results.Add("StraightFlush5Pair");
							}
						}
						else
						{
							// The non-wheel case.
							if (flushCards[0].RankValue == notFlushCards[0].RankValue ||
								flushCards[5].RankValue == notFlushCards[0].RankValue)
							{
								results.Add("StraightFlush5Pair");
							}
						}
					}
					else if (strFlushLen == 5)
					{
						// Can we form a Pair with the suited card that isn't part of our
						// StrFl5 and the non-suited card?

						// High-end StrFl5 (EX: AKQJT2)
						if (flushCards[0].RankValue == (flushCards[1].RankValue + 1) &&
							flushCards[5].RankValue == notFlushCards[0].RankValue)
						{
							results.Add("StraightFlush5Pair");
						}
						// The Wheel (EX: AK5432)
						else if (flushCards[0].RankValue == 14 &&
							flushCards[1].RankValue == notFlushCards[0].RankValue)
						{
							results.Add("StraightFlush5Pair");
						}
						// Low-end StrFl5 (EX: T87654)
						else if (flushCards[0].RankValue == notFlushCards[0].RankValue)
						{
							results.Add("StraightFlush5Pair");
						}
						// No Pair to go with the StrFl5.
						else
							results.Add("StraightFlush5");
					}
					else
					{
						// We do not have a straight flush withing our 6 suited cards.

						// If one of our suited cards pairs the non-suited cards,
						// then we also have a Flush5Pair.
						foreach (Card card in flushCards)
							if (card.RankValue == notFlushCards[0].RankValue)
							{
								results.Add("Flush5Pair");
								break;
							}
					}
				}
				else if (longestFlush == 5)
				{
					// First look for straight flushes.
					int strFlushLen = 0;
					List<Card> flushCards = null;
					switch (longestFlushSuit)
					{
						case "c":
							strFlushLen = LargestStraight(clubs);
							flushCards = clubs;
							break;
						case "d":
							strFlushLen = LargestStraight(diamonds);
							flushCards = diamonds;
							break;
						case "h":
							strFlushLen = LargestStraight(hearts);
							flushCards = hearts;
							break;
						case "s":
							strFlushLen = LargestStraight(spades);
							flushCards = spades;
							break;
					}
					List<Card> notFlushCards = cards.Except(flushCards).ToList();
					if (strFlushLen == 5)
					{
						// If the non-suited cards are of the same rank,
						// then we also have a StraightFlush5Pair.
						if (notFlushCards[0].RankValue == notFlushCards[1].RankValue)
						{
							results.Add("StraightFlush5Pair");
						}
						// No Pair to go with the StrFl5.
						else
							results.Add("StraightFlush5");
					}
					else
					{
						// Not a straight flush.
						// If the non-suited cards are of the same rank,
						// then we have a Flush5Pair; otherwise just Flush5.
						if (notFlushCards[0].RankValue == notFlushCards[1].RankValue)
							results.Add("Flush5Pair");
						else
							results.Add("Flush5");
					}
				}

				// Now look for straights.
				// At this point all flush combinations have been identified, so
				// there is no need to look for them here.
				if (longestStraight == 7 && !results.Contains("StraightFlush7"))
					results.Add("Straight7");
				else if (longestStraight == 6)
				{
					// Don't count Str6 if we have already identified StrFl6.
					if (!results.Contains("StraightFlush6"))
						results.Add("Straight6");

					// If either of the end cards matches the 7th card then we also
					// have a Str5Pair.
					// However, if we have already identified a StrFl5Pair then
					// there is no need to proceed, since a Str5Pair is a subset
					// (in the same way that we are not also reporting Flush5Pair).
					if (!results.Contains("StraightFlush5Pair"))
					{
						// Create a collection of the cards used in the straight.
						// Because we have already accounted for flushes, the suit
						// doesn't matter (no need to worry about breaking up a StrFl).
						int runLowestRank;
						if (runHighestRank == 6)
							runLowestRank = 2; // Ace is 14, not 1, which would kill our loop
						else
							runLowestRank = runHighestRank - 5;
						List<Card> straightCards = new List<Card>();
						for (int i = runHighestRank; i >= runLowestRank; i--)
						{
							straightCards.Add(cards.Find(c => c.RankValue == i));
						}
						if (runHighestRank == 6)
						{
							// We know the first card must be an Ace.
							straightCards.Add(cards[0]);
						}

						// Get the card that isn't in the straight.
						List<Card> notStraightCards = cards.Except(straightCards).ToList();

						// Compare that card to the endpoints of the straight.
						if (straightCards[0].RankValue == notStraightCards[0].RankValue ||
							straightCards[5].RankValue == notStraightCards[0].RankValue)
							results.Add("Straight5Pair");
					}
				}
				else if (longestStraight == 5)
				{
					// If we have already found a StrFl5 or StrFl5Pair, then
					// there is nothing new to find.
					if (!results.Contains("StraightFlush5") &&
						!results.Contains("StraightFlush5Pair"))
					{
						bool str5Pair = false;

						// Might also have a Pair, so check the two that aren't involved.
						int possiblePairRank = -1;

						// If it is a straight to the 5, then it is The Wheel, which
						// means that the first card (at least) is an Ace. Pop it off
						// the beginning of the list and put it at the end.
						if (runHighestRank == 5)
						{
							Card ace = cards[0];
							cards.RemoveAt(0);
							cards.Add(ace);
						}

						for (int i = 0; i < 7; i++)
						{
							if (cards[i].RankValue == runHighestRank)
							{
								// This card is part of the straight, so we are not interested.
								// We are now looking for the next-lowest rank.
								runHighestRank--;
								// Handle The Wheel.
								if (runHighestRank == 1)
									runHighestRank = 14;
							}
							else
							{
								// This card is not part of the straight.
								// If this is the first such card we have encountered, remember its rank.
								if (possiblePairRank == -1)
									possiblePairRank = cards[i].RankValue;
								else if (possiblePairRank == cards[i].RankValue)
								{
									results.Add("Straight5Pair");
									str5Pair = true;
								}
							}
						}

						// Straight5 is a subset of Straight5Pair, so don't allow both.
						if (!str5Pair)
							results.Add("Straight5");
					}
				}
				return results;
			}

			// Now we can handle matches.
			// Try some funky math to make things simpler/faster???
			int value = numQuad * 16 + numSet * 4 + numPair;
			switch (value)
			{
				case 20:
					results.Add("QuadSet");
					break;
				case 17:
					results.Add("QuadPair");
					break;
				case 16:
					results.Add("Quad");
					break;
				case 8:
					results.Add("SetSet");
					break;
				case 6:
					results.Add("SetPairPair");
					break;
				case 5:
					results.Add("SetPair");
					break;
				case 4:
					results.Add("Set");
					break;
				case 3:
					results.Add("PairPairPair");
					break;
				case 2:
					results.Add("PairPair");
					break;
				case 1:
					results.Add("Pair");
					break;
				default:
					results.Add("HighCard");
					break;
			}

			return results;
		}
	}
}
