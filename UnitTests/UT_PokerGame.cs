using ConsoleSamples.Casino_Sample;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Xunit;
using Xunit.Abstractions;

namespace UnitTests
{
	public class GamingFixture
	{
		static public Card AceOfSpades = new Card("A", "s");
		static public Card KingOfSpades = new Card("K", "s");
		static public Card QueenOfSpades = new Card("Q", "s");
		static public Card JackOfSpades = new Card("J", "s");
		static public Card TenOfSpades = new Card("T", "s");
		static public Card NineOfSpades = new Card("9", "s");
		static public Card EightOfSpades = new Card("8", "s");
		static public Card SevenOfSpades = new Card("7", "s");
		static public Card SixOfSpades = new Card("6", "s");
		static public Card FiveOfSpades = new Card("5", "s");
		static public Card FourOfSpades = new Card("4", "s");
		static public Card ThreeOfSpades = new Card("3", "s");
		static public Card TwoOfSpades = new Card("2", "s");

		static public Card AceOfHearts = new Card("A", "h");
		static public Card KingOfHearts = new Card("K", "h");
		static public Card QueenOfHearts = new Card("Q", "h");
		static public Card JackOfHearts = new Card("J", "h");
		static public Card TenOfHearts = new Card("T", "h");
		static public Card NineOfHearts = new Card("9", "h");
		static public Card EightOfHearts = new Card("8", "h");
		static public Card SevenOfHearts = new Card("7", "h");
		static public Card SixOfHearts = new Card("6", "h");
		static public Card FiveOfHearts = new Card("5", "h");
		static public Card FourOfHearts = new Card("4", "h");
		static public Card ThreeOfHearts = new Card("3", "h");
		static public Card TwoOfHearts = new Card("2", "h");

		static public Card AceOfDiamonds = new Card("A", "d");
		static public Card KingOfDiamonds = new Card("K", "d");
		static public Card QueenOfDiamonds = new Card("Q", "d");
		static public Card JackOfDiamonds = new Card("J", "d");
		static public Card TenOfDiamonds = new Card("T", "d");
		static public Card NineOfDiamonds = new Card("9", "d");
		static public Card EightOfDiamonds = new Card("8", "d");
		static public Card SevenOfDiamonds = new Card("7", "d");
		static public Card SixOfDiamonds = new Card("6", "d");
		static public Card FiveOfDiamonds = new Card("5", "d");
		static public Card FourOfDiamonds = new Card("4", "d");
		static public Card ThreeOfDiamonds = new Card("3", "d");
		static public Card TwoOfDiamonds = new Card("2", "d");

		static public Card AceOfClubs = new Card("A", "c");
		static public Card KingOfClubs = new Card("K", "c");
		static public Card QueenOfClubs = new Card("Q", "c");
		static public Card JackOfClubs = new Card("J", "c");
		static public Card TenOfClubs = new Card("T", "c");
		static public Card NineOfClubs = new Card("9", "c");
		static public Card EightOfClubs = new Card("8", "c");
		static public Card SevenOfClubs = new Card("7", "c");
		static public Card SixOfClubs = new Card("6", "c");
		static public Card FiveOfClubs = new Card("5", "c");
		static public Card FourOfClubs = new Card("4", "c");
		static public Card ThreeOfClubs = new Card("3", "c");
		static public Card TwoOfClubs = new Card("2", "c");

		static public Card[] Shuffle(Card[] Cards)
		{
			Random rng = new Random();
			for (int i = 0; i < Cards.Length; i++)
			{
				int rand = rng.Next(Cards.Length - i);
				Card temp = Cards[i];
				Cards[i] = Cards[i + rand];
				Cards[i + rand] = temp;
			}

			return Cards;
		}
	}

	public class UT_PokerGame : IClassFixture<GamingFixture>
	{
		private readonly ITestOutputHelper Output;

		public UT_PokerGame(ITestOutputHelper cout)
		{
			Output = cout;
		}

		[Fact]
		public void EvaluateSevenCardHand_5CardFlush()
		{
			// ASSEMBLE
			List<Card> actCards = new List<Card>
			{
				GamingFixture.EightOfClubs,
				GamingFixture.FiveOfClubs,
				GamingFixture.FourOfClubs,
				GamingFixture.NineOfClubs,
				GamingFixture.SevenOfHearts,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.TwoOfClubs
			};

			// ACT
			int handStrength = PokerGame.GetBest_5From7_All21(actCards);
			Output.WriteLine($"Calculated strength: {handStrength:X8}");

			// ASSERT
			Assert.Equal(0x00598542, handStrength);
		}

		[Fact]
		public void EvaluateSevenCardHand_6CardFlush()
		{
			// ASSEMBLE
			List<Card> actCards = new List<Card>
			{
				GamingFixture.EightOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.NineOfDiamonds,
				GamingFixture.SevenOfHearts,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.TwoOfDiamonds
			};

			// ACT
			int handStrength = PokerGame.GetBest_5From7_All21(actCards);
			Output.WriteLine($"Calculated strength: {handStrength:X8}");

			// ASSERT
			Assert.Equal(0x00598543, handStrength);
		}

		[Fact]
		public void EvaluateSevenCardHand_7CardFlush()
		{
			// ASSEMBLE
			List<Card> actCards = new List<Card>
			{
				GamingFixture.AceOfHearts,
				GamingFixture.EightOfHearts,
				GamingFixture.FourOfHearts,
				GamingFixture.JackOfHearts,
				GamingFixture.NineOfHearts,
				GamingFixture.ThreeOfHearts,
				GamingFixture.TwoOfHearts
			};

			// ACT
			int handStrength = PokerGame.GetBest_5From7_All21(actCards);
			Output.WriteLine($"Calculated strength: {handStrength:X8}");

			// ASSERT
			Assert.Equal(0x005EB984, handStrength);
		}

		[Fact]
		public void EvaluateSevenCardHand_5CardStraight()
		{
			// ASSEMBLE
			List<Card> actCards = new List<Card>
			{
				GamingFixture.AceOfDiamonds,
				GamingFixture.EightOfDiamonds,
				GamingFixture.JackOfClubs,
				GamingFixture.NineOfSpades,
				GamingFixture.QueenOfHearts,
				GamingFixture.TenOfSpades,
				GamingFixture.TwoOfClubs
			};

			// ACT
			int handStrength = PokerGame.GetBest_5From7_All21(actCards);
			Output.WriteLine($"Calculated strength: {handStrength:X8}");

			// ASSERT
			Assert.Equal(0x004CBA98, handStrength);
		}

		[Fact]
		public void EvaluateSevenCardHand_5CardStraight_NotContiguous()
		{
			// ASSEMBLE
			List<Card> actCards = new List<Card>
			{
				GamingFixture.SixOfDiamonds,
				GamingFixture.SixOfHearts,
				GamingFixture.FiveOfClubs,
				GamingFixture.FiveOfSpades,
				GamingFixture.FourOfHearts,
				GamingFixture.ThreeOfSpades,
				GamingFixture.TwoOfClubs
			};

			// ACT
			int handStrength = PokerGame.GetBest_5From7_All21(actCards);
			Output.WriteLine($"Calculated strength: {handStrength:X8}");

			// ASSERT
			Assert.Equal(0x00465432, handStrength);
		}

		[Fact]
		public void EvaluateSevenCardHand_5CardStraight_TheWheel()
		{
			// ASSEMBLE
			List<Card> actCards = new List<Card>
			{
				GamingFixture.SevenOfDiamonds,
				GamingFixture.AceOfDiamonds,
				GamingFixture.FiveOfClubs,
				GamingFixture.FiveOfSpades,
				GamingFixture.FourOfHearts,
				GamingFixture.ThreeOfSpades,
				GamingFixture.TwoOfClubs
			};

			// ACT
			int handStrength = PokerGame.GetBest_5From7_All21(actCards);
			Output.WriteLine($"Calculated strength: {handStrength:X8}");

			// ASSERT
			Assert.Equal(0x00454321, handStrength);
		}

		[Fact]
		public void EvaluateSevenCardHand_6CardStraight_NotTheWheel()
		{
			// ASSEMBLE
			List<Card> actCards = new List<Card>
			{
				GamingFixture.SixOfDiamonds,
				GamingFixture.TwoOfDiamonds,
				GamingFixture.FiveOfClubs,
				GamingFixture.AceOfSpades,
				GamingFixture.FourOfHearts,
				GamingFixture.ThreeOfSpades,
				GamingFixture.TwoOfClubs
			};

			// ACT
			int handStrength = PokerGame.GetBest_5From7_All21(actCards);
			Output.WriteLine($"Calculated strength: {handStrength:X8}");

			// ASSERT
			Assert.Equal(0x00465432, handStrength);
		}

		[Fact]
		public void EvaluateSevenCardHand_StraightFlush()
		{
			// ASSEMBLE
			List<Card> actCards = new List<Card>
			{
				GamingFixture.SixOfDiamonds,
				GamingFixture.SevenOfDiamonds,
				GamingFixture.EightOfDiamonds,
				GamingFixture.NineOfDiamonds,
				GamingFixture.TenOfDiamonds,
				GamingFixture.KingOfSpades,
				GamingFixture.FourOfClubs,
			};

			// ACT
			//int handStrength = PokerGame.EvaluateSevenCardHand(actCards);
			int handStrength = PokerGame.GetBest_5From7_All21(actCards);
			Output.WriteLine($"Calculated strength: {handStrength:X8}");

			// ASSERT
			Assert.Equal(0x008A9876, handStrength);
		}

		[Fact]
		public void EvaluateSevenCardHand_StraightFlush_TheWheel()
		{
			// ASSEMBLE
			List<Card> actCards = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.AceOfDiamonds,
				GamingFixture.KingOfSpades,
				GamingFixture.FourOfClubs,
			};

			// ACT
			int handStrength = PokerGame.GetBest_5From7_All21(actCards);
			Output.WriteLine($"Calculated strength: {handStrength:X8}");

			// ASSERT
			Assert.Equal(0x00854321, handStrength);
		}

		[Fact]
		public void EvaluateSevenCardHand_StraightFlush_TheWheelPlus()
		{
			// ASSEMBLE
			List<Card> actCards = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.AceOfDiamonds,
				GamingFixture.KingOfSpades,
				GamingFixture.SixOfDiamonds,
			};

			// ACT
			int handStrength = PokerGame.GetBest_5From7_All21(actCards);
			Output.WriteLine($"Calculated strength: {handStrength:X8}");

			// ASSERT
			Assert.Equal(0x00865432, handStrength);
		}

		[Fact]
		public void EvaluateSevenCardHand_StraightFlush_Broadway()
		{
			// ASSEMBLE
			List<Card> actCards = new List<Card>
			{
				GamingFixture.TenOfHearts,
				GamingFixture.JackOfHearts,
				GamingFixture.QueenOfHearts,
				GamingFixture.KingOfHearts,
				GamingFixture.AceOfHearts,
				GamingFixture.KingOfSpades,
				GamingFixture.FourOfClubs,
			};

			// ACT
			int handStrength = PokerGame.GetBest_5From7_All21(actCards);
			Output.WriteLine($"Calculated strength: {handStrength:X8}");

			// ASSERT
			Assert.Equal(0x008EDCBA, handStrength);
		}

		[Fact]
		public void EvaluateSevenCardHand_StraightFlush_BroadwayPlus()
		{
			// ASSEMBLE
			List<Card> actCards = new List<Card>
			{
				GamingFixture.TenOfHearts,
				GamingFixture.JackOfHearts,
				GamingFixture.QueenOfHearts,
				GamingFixture.KingOfHearts,
				GamingFixture.AceOfHearts,
				GamingFixture.KingOfSpades,
				GamingFixture.NineOfHearts,
			};

			// ACT
			int handStrength = PokerGame.GetBest_5From7_All21(actCards);
			Output.WriteLine($"Calculated strength: {handStrength:X8}");

			// ASSERT
			Assert.Equal(0x008EDCBA, handStrength);
		}

		[Fact]
		public void EvaluateSevenCardHand_FourOfAKind()
		{
			// ASSEMBLE
			List<Card> actCards = new List<Card>
			{
				GamingFixture.TenOfHearts,
				GamingFixture.JackOfHearts,
				GamingFixture.JackOfClubs,
				GamingFixture.KingOfHearts,
				GamingFixture.JackOfDiamonds,
				GamingFixture.KingOfSpades,
				GamingFixture.JackOfSpades,
			};

			// ACT
			int handStrength = PokerGame.GetBest_5From7_All21(actCards);
			Output.WriteLine($"Calculated strength: {handStrength:X8}");

			// ASSERT
			Assert.Equal(0x007BBBBD, handStrength);
		}

		[Fact]
		public void EvaluateSevenCardHand_FullHouse()
		{
			// ASSEMBLE
			List<Card> actCards = new List<Card>
			{
				GamingFixture.TenOfHearts,
				GamingFixture.JackOfHearts,
				GamingFixture.JackOfClubs,
				GamingFixture.KingOfHearts,
				GamingFixture.SixOfDiamonds,
				GamingFixture.KingOfSpades,
				GamingFixture.JackOfSpades,
			};

			// ACT
			int handStrength = PokerGame.GetBest_5From7_All21(actCards);
			Output.WriteLine($"Calculated strength: {handStrength:X8}");

			// ASSERT
			Assert.Equal(0x006BBBDD, handStrength);
		}

		[Fact]
		public void EvaluateSevenCardHand_ThreeOfAKind()
		{
			// ASSEMBLE
			List<Card> actCards = new List<Card>
			{
				GamingFixture.TenOfHearts,
				GamingFixture.FiveOfHearts,
				GamingFixture.KingOfClubs,
				GamingFixture.KingOfHearts,
				GamingFixture.SixOfHearts,
				GamingFixture.KingOfSpades,
				GamingFixture.JackOfDiamonds,
			};

			// ACT
			int handStrength = PokerGame.GetBest_5From7_All21(actCards);
			Output.WriteLine($"Calculated strength: {handStrength:X8}");

			// ASSERT
			Assert.Equal(0x003DDDBA, handStrength);
		}

		[Fact]
		public void EvaluateSevenCardHand_ThreePair()
		{
			// ASSEMBLE
			List<Card> actCards = new List<Card>
			{
				GamingFixture.SixOfHearts,
				GamingFixture.JackOfHearts,
				GamingFixture.JackOfClubs,
				GamingFixture.KingOfHearts,
				GamingFixture.SixOfDiamonds,
				GamingFixture.KingOfSpades,
				GamingFixture.AceOfSpades,
			};

			// ACT
			int handStrength = PokerGame.GetBest_5From7_All21(actCards);
			Output.WriteLine($"Calculated strength: {handStrength:X8}");

			// ASSERT
			Assert.Equal(0x002DDBBE, handStrength);
		}

		[Fact]
		public void EvaluateSevenCardHand_TwoPair()
		{
			// ASSEMBLE
			List<Card> actCards = new List<Card>
			{
				GamingFixture.TenOfHearts,
				GamingFixture.JackOfHearts,
				GamingFixture.JackOfClubs,
				GamingFixture.KingOfHearts,
				GamingFixture.SixOfDiamonds,
				GamingFixture.KingOfSpades,
				GamingFixture.AceOfSpades,
			};

			// ACT
			int handStrength = PokerGame.GetBest_5From7_All21(actCards);
			Output.WriteLine($"Calculated strength: {handStrength:X8}");

			// ASSERT
			Assert.Equal(0x002DDBBE, handStrength);
		}

		[Fact]
		public void EvaluateSevenCardHand_OnePair()
		{
			// ASSEMBLE
			List<Card> actCards = new List<Card>
			{
				GamingFixture.TenOfHearts,
				GamingFixture.ThreeOfHearts,
				GamingFixture.FourOfClubs,
				GamingFixture.NineOfHearts,
				GamingFixture.SixOfDiamonds,
				GamingFixture.ThreeOfSpades,
				GamingFixture.AceOfSpades,
			};

			// ACT
			int handStrength = PokerGame.GetBest_5From7_All21(actCards);
			Output.WriteLine($"Calculated strength: {handStrength:X8}");

			// ASSERT
			Assert.Equal(0x00133EA9, handStrength);
		}

		[Fact]
		public void EvaluateSevenCardHand_HighCardAce()
		{
			// ASSEMBLE
			List<Card> actCards = new List<Card>
			{
				GamingFixture.TenOfHearts,
				GamingFixture.ThreeOfHearts,
				GamingFixture.FourOfClubs,
				GamingFixture.AceOfSpades,
				GamingFixture.NineOfHearts,
				GamingFixture.SixOfDiamonds,
				GamingFixture.SevenOfSpades,
			};

			// ACT
			int handStrength = PokerGame.GetBest_5From7_All21(actCards);
			Output.WriteLine($"Calculated strength: {handStrength:X8}");

			// ASSERT
			Assert.Equal(0x000EA976, handStrength);
		}

		[Fact]
		public void EvaluateSevenCardHand_HighCardNine()
		{
			// ASSEMBLE
			List<Card> actCards = new List<Card>
			{
				GamingFixture.TwoOfHearts,
				GamingFixture.ThreeOfHearts,
				GamingFixture.FourOfClubs,
				GamingFixture.FiveOfSpades,
				GamingFixture.NineOfHearts,
				GamingFixture.EightOfDiamonds,
				GamingFixture.SevenOfSpades,
			};

			// ACT
			int handStrength = PokerGame.GetBest_5From7_All21(actCards);
			Output.WriteLine($"Calculated strength: {handStrength:X8}");

			// ASSERT
			Assert.Equal(0x00098754, handStrength);
		}

		[Fact]
		public void GetNumDistinctRanks()
		{
			List<Card> cards = new List<Card>();

			cards.Add(GamingFixture.AceOfClubs);
			Assert.Equal(1, PokerGame.GetNumDistinctRanks(cards));

			cards.Add(GamingFixture.AceOfDiamonds);
			Assert.Equal(1, PokerGame.GetNumDistinctRanks(cards));

			cards.Add(GamingFixture.KingOfDiamonds);
			Assert.Equal(2, PokerGame.GetNumDistinctRanks(cards));

			// Method doesn't enforce unique cards in the list.
			cards.Add(GamingFixture.KingOfDiamonds);
			cards.Add(GamingFixture.KingOfDiamonds);
			cards.Add(GamingFixture.KingOfDiamonds);
			cards.Add(GamingFixture.KingOfDiamonds);
			cards.Add(GamingFixture.KingOfDiamonds);
			cards.Add(GamingFixture.KingOfDiamonds);
			Assert.Equal(2, PokerGame.GetNumDistinctRanks(cards));

			cards.Add(GamingFixture.QueenOfDiamonds);
			Assert.Equal(3, PokerGame.GetNumDistinctRanks(cards));

			cards.Add(GamingFixture.TwoOfDiamonds);
			cards.Add(GamingFixture.ThreeOfDiamonds);
			cards.Add(GamingFixture.FourOfDiamonds);
			cards.Add(GamingFixture.FiveOfDiamonds);
			cards.Add(GamingFixture.SixOfDiamonds);
			cards.Add(GamingFixture.SevenOfDiamonds);
			cards.Add(GamingFixture.EightOfDiamonds);
			Assert.Equal(10, PokerGame.GetNumDistinctRanks(cards));

			cards.Add(GamingFixture.TwoOfDiamonds);
			cards.Add(GamingFixture.ThreeOfDiamonds);
			cards.Add(GamingFixture.NineOfDiamonds);
			cards.Add(GamingFixture.FiveOfDiamonds);
			cards.Add(GamingFixture.TenOfDiamonds);
			cards.Add(GamingFixture.SevenOfDiamonds);
			cards.Add(GamingFixture.JackOfDiamonds);
			Assert.Equal(13, PokerGame.GetNumDistinctRanks(cards));
		}

		[Fact]
		public void IsStraight_Basic()
		{
			Assert.False(PokerGame.IsStraight(null));

			List<Card> hand = new List<Card>();

			Assert.False(PokerGame.IsStraight(hand));

			hand.Add(GamingFixture.SixOfDiamonds);
			// We are responsible for sorting from high to low.
			hand.Sort();
			hand.Reverse();
			Assert.True(PokerGame.IsStraight(hand));

			hand.Add(GamingFixture.FiveOfDiamonds);
			// We are responsible for sorting from high to low.
			hand.Sort();
			hand.Reverse();
			Assert.True(PokerGame.IsStraight(hand));

			hand.Add(GamingFixture.SevenOfDiamonds);
			// We are responsible for sorting from high to low.
			hand.Sort();
			hand.Reverse();
			Assert.True(PokerGame.IsStraight(hand));

			hand.Add(GamingFixture.TwoOfDiamonds);
			// We are responsible for sorting from high to low.
			hand.Sort();
			hand.Reverse();
			Assert.False(PokerGame.IsStraight(hand));

			hand.Add(GamingFixture.ThreeOfDiamonds);
			// We are responsible for sorting from high to low.
			hand.Sort();
			hand.Reverse();
			Assert.False(PokerGame.IsStraight(hand));

			hand.Add(GamingFixture.AceOfDiamonds);
			// We are responsible for sorting from high to low.
			hand.Sort();
			hand.Reverse();
			Assert.False(PokerGame.IsStraight(hand));

			// This completes The Wheel.
			hand.Add(GamingFixture.FourOfDiamonds);
			// We are responsible for sorting from high to low.
			hand.Sort();
			hand.Reverse();
			Assert.True(PokerGame.IsStraight(hand)); // The Wheel
		}

		[Fact]
		public void GetCombos()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.AceOfClubs,
				GamingFixture.EightOfClubs,
				GamingFixture.FiveOfClubs,
				GamingFixture.FourOfClubs,
				GamingFixture.JackOfClubs,
				GamingFixture.KingOfClubs,
				GamingFixture.NineOfClubs,
			};

			List<List<Card>> allCombos = PokerGame.GetCombos(hand, 5);
			Output.WriteLine($"Here are the {allCombos.Count} combinations:");
			foreach (List<Card> combo in allCombos)
			{
				string output = "Cards: ";
				foreach (Card card in combo)
					output += $"{card.Rank}{card.Suit} ";
				Output.WriteLine($"{output}");
			}
		}

		#region Base: 7 Suited Cards
		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_7SuitedCards_StrFl7()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.EightOfClubs,
				GamingFixture.FiveOfClubs,
				GamingFixture.FourOfClubs,
				GamingFixture.NineOfClubs,
				GamingFixture.SevenOfClubs,
				GamingFixture.SixOfClubs,
				GamingFixture.TenOfClubs,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("StraightFlush7");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_7SuitedCards_StrFl7_TheWheel()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.AceOfClubs,
				GamingFixture.FiveOfClubs,
				GamingFixture.FourOfClubs,
				GamingFixture.TwoOfClubs,
				GamingFixture.SevenOfClubs,
				GamingFixture.SixOfClubs,
				GamingFixture.ThreeOfClubs,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("StraightFlush7");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}
		
		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_7SuitedCards_Fl7()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.EightOfClubs,
				GamingFixture.FiveOfClubs,
				GamingFixture.FourOfClubs,
				GamingFixture.NineOfClubs,
				GamingFixture.TwoOfClubs,
				GamingFixture.SixOfClubs,
				GamingFixture.TenOfClubs,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush7");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_7SuitedCards_StrFl6()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.AceOfClubs,
				GamingFixture.EightOfClubs,
				GamingFixture.FiveOfClubs,
				GamingFixture.FourOfClubs,
				GamingFixture.NineOfClubs,
				GamingFixture.SevenOfClubs,
				GamingFixture.SixOfClubs,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush7");
			expResults.Add("StraightFlush6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_7SuitedCards_StrFl6_TheWheel()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.AceOfClubs,
				GamingFixture.TwoOfClubs,
				GamingFixture.FiveOfClubs,
				GamingFixture.FourOfClubs,
				GamingFixture.ThreeOfClubs,
				GamingFixture.EightOfClubs,
				GamingFixture.SixOfClubs,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush7");
			expResults.Add("StraightFlush6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_7SuitedCards_StrFl5()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.AceOfClubs,
				GamingFixture.EightOfClubs,
				GamingFixture.FiveOfClubs,
				GamingFixture.KingOfClubs,
				GamingFixture.NineOfClubs,
				GamingFixture.SevenOfClubs,
				GamingFixture.SixOfClubs,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush7");
			expResults.Add("StraightFlush5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_7SuitedCards_StrFl5_TheWheel()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.QueenOfClubs,
				GamingFixture.FiveOfClubs,
				GamingFixture.FourOfClubs,
				GamingFixture.AceOfClubs,
				GamingFixture.KingOfClubs,
				GamingFixture.ThreeOfClubs,
				GamingFixture.TwoOfClubs,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush7");
			expResults.Add("StraightFlush5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}
		#endregion

		#region Base: 6 Suited Cards
		//
		// 7-card constraint: Straight7
		// Can include the following additional hands:
		// - StraightFlush6
		// - StraightFlush5
		// - Flush6 (implied by the Base scenario)
		// Cannot include StraightFlush5Pair because all ranks must be unique to make Straight7.
		//
		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_Str7_StrFl6()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.SixOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.TwoOfDiamonds,
				GamingFixture.SevenOfDiamonds,
				GamingFixture.EightOfHearts,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight7");
			expResults.Add("StraightFlush6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_Str7_TheWheel_StrFl6()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.SixOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.TwoOfDiamonds,
				GamingFixture.SevenOfDiamonds,
				GamingFixture.AceOfHearts,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight7");
			expResults.Add("StraightFlush6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_Str7_StrFl6_TheWheel()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.SixOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.TwoOfDiamonds,
				GamingFixture.AceOfDiamonds,
				GamingFixture.SevenOfHearts,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight7");
			expResults.Add("StraightFlush6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_Str7_StrFl5()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfHearts,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfDiamonds,
				GamingFixture.SevenOfDiamonds,
				GamingFixture.EightOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight7");
			expResults.Add("Flush6");
			expResults.Add("StraightFlush5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		// public void FindFlushesAndStraights_6SuitedCards_Str7_TheWheel_StrFl5()
		//
		// This case is not possible. For the StrFl to not be The Wheel, the Ace
		// must be the non-suited card. But this means that the 2 - 7 must be suited,
		// which is a 6-card StrFl instead of a 5-card StrFl.

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_Str7_StrFl5_TheWheel()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.AceOfDiamonds,
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfHearts,
				GamingFixture.SevenOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight7");
			expResults.Add("Flush6");
			expResults.Add("StraightFlush5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_Str7()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.SixOfHearts,
				GamingFixture.FiveOfClubs,
				GamingFixture.FourOfHearts,
				GamingFixture.ThreeOfHearts,
				GamingFixture.TwoOfHearts,
				GamingFixture.SevenOfHearts,
				GamingFixture.EightOfHearts,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight7");
			expResults.Add("Flush6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_Str7_TheWheel()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.SixOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.FourOfClubs,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.TwoOfDiamonds,
				GamingFixture.SevenOfDiamonds,
				GamingFixture.AceOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight7");
			expResults.Add("Flush6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		//
		// 6-card constraint: Straight6
		// Can include the following additional hands:
		// - StraightFlush6
		// - StraightFlush5Pair (breaking the StraightFlush6 e.g. [234567]7)
		// - Flush6 (implied by the Base scenario)
		// Cannot include StraightFlush5Pair because all ranks must be unique to make Straight7.
		//
		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_Str6_StrFl6()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.SixOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.TwoOfDiamonds,
				GamingFixture.SevenOfDiamonds,
				GamingFixture.NineOfHearts,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("StraightFlush6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_Str6_StrFl6_TheWheel()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.SixOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.TwoOfDiamonds,
				GamingFixture.AceOfDiamonds,
				GamingFixture.EightOfHearts,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("StraightFlush6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_StrFl6_StrFl5Pair()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.SevenOfDiamonds,
				GamingFixture.SixOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.TwoOfSpades,
				GamingFixture.TwoOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("StraightFlush6");
			expResults.Add("StraightFlush5Pair");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_StrFl6_TheWheelLow_StrFl5Pair()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.SixOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.TwoOfDiamonds,
				GamingFixture.AceOfDiamonds,
				GamingFixture.AceOfSpades,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("StraightFlush6");
			expResults.Add("StraightFlush5Pair");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_StrFl6_TheWheelHigh_StrFl5Pair()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.SixOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.TwoOfDiamonds,
				GamingFixture.AceOfDiamonds,
				GamingFixture.SixOfSpades,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("StraightFlush6");
			expResults.Add("StraightFlush5Pair");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_StrFl6_StrFl5Pair_TheWheel()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.SixOfHearts,
				GamingFixture.SixOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.TwoOfDiamonds,
				GamingFixture.AceOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("StraightFlush6");
			expResults.Add("StraightFlush5Pair");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_Str6()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.SevenOfDiamonds,
				GamingFixture.SixOfDiamonds,
				GamingFixture.FiveOfClubs,
				GamingFixture.FourOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.TwoOfDiamonds,
				GamingFixture.NineOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight6");
			expResults.Add("Flush6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_Str6_TheWheel()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.EightOfDiamonds,
				GamingFixture.SixOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.FourOfClubs,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.TwoOfDiamonds,
				GamingFixture.AceOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight6");
			expResults.Add("Flush6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}


		//
		// Base Case: 6 suited cards
		// 5-card constraint: Straight5
		// Include the following hands:
		// - StraightFlush5Pair
		// - StraightFlush5
		// - Flush6(implied by the Base Case)
		// - Straight5 (implied by the Constraint)
		// Cannot include the following hands:
		// - Straight5Pair, because the non-suited card must be used in the Str5 to prevent it
		//   from being a StrFl5 (which is a different permutation); the remaining two cards are
		//   thus the same suit, which means they can't possibly form a pair.
		//
		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_StrFl5Pair() // scenario 1
		{
			// The Pair does not break a StrFl6.
			List<Card> hand = new List<Card>
			{
				GamingFixture.EightOfDiamonds,
				GamingFixture.NineOfDiamonds,
				GamingFixture.TenOfDiamonds,
				GamingFixture.JackOfDiamonds,
				GamingFixture.QueenOfDiamonds,
				GamingFixture.TwoOfDiamonds,
				GamingFixture.TwoOfHearts,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush6");
			expResults.Add("StraightFlush5Pair");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_StrFl5Pair_TheWheel() // scenario 2
		{
			// The Pair does not break a StrFl6.
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.AceOfDiamonds,
				GamingFixture.TenOfDiamonds,
				GamingFixture.TenOfHearts,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush6");
			expResults.Add("StraightFlush5Pair");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_StrFl5Pair_LowEnd() // scenario 3
		{
			// The Pair does not break a StrFl6.
			List<Card> hand = new List<Card>
			{
				GamingFixture.EightOfDiamonds,
				GamingFixture.NineOfDiamonds,
				GamingFixture.TenOfDiamonds,
				GamingFixture.JackOfDiamonds,
				GamingFixture.QueenOfDiamonds,
				GamingFixture.AceOfDiamonds,
				GamingFixture.AceOfHearts,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush6");
			expResults.Add("StraightFlush5Pair");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_StrFl5() // scenario 4
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.EightOfDiamonds,
				GamingFixture.NineOfDiamonds,
				GamingFixture.TenOfDiamonds,
				GamingFixture.JackOfDiamonds,
				GamingFixture.QueenOfDiamonds,
				GamingFixture.AceOfDiamonds,
				GamingFixture.TwoOfHearts,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush6");
			expResults.Add("StraightFlush5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_StrFl5_TheWheel()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.AceOfDiamonds,
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.QueenOfDiamonds,
				GamingFixture.SevenOfHearts,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush6");
			expResults.Add("StraightFlush5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_Str5()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfSpades,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfDiamonds,
				GamingFixture.QueenOfDiamonds,
				GamingFixture.EightOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush6");
			expResults.Add("Straight5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_Str5_TheWheel()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.AceOfDiamonds,
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfSpades,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.QueenOfDiamonds,
				GamingFixture.SevenOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush6");
			expResults.Add("Straight5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}



		//
		// Base Case: 6 suited cards
		// Constraint: none
		// Can include the following additional hands:
		// - Flush5Pair
		// - Flush6 (implied by the Base scenario)
		// Cannot include the following hands:
		// - Straight 5 or longer, because that would be a constraint.
		// - StraightFlush 5 or 6, because there are no straights of any kind.
		// - More than a single Pair, because that would violate the Base Case (6 unique ranks).
		//
		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_Fl5Pair()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfDiamonds,
				GamingFixture.EightOfDiamonds,
				GamingFixture.TenOfDiamonds,
				GamingFixture.TenOfSpades,
				GamingFixture.QueenOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush5Pair");
			expResults.Add("Flush6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6SuitedCards_Fl6()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfSpades,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfDiamonds,
				GamingFixture.EightOfDiamonds,
				GamingFixture.TenOfDiamonds,
				GamingFixture.QueenOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}
		#endregion

		#region Base: 5 suited cards
		//
		// Base Case: 5 suited cards
		// 7-card constraint: Straight7
		// Can include the following additional hands:
		// - StraightFlush5
		// - Flush5 (implied by the Base scenario)
		// Cannot include StraightFlush5Pair because all ranks must be unique to make Straight7.
		// Cannot include Flush5Pair because all ranks must be unique to make Straight7.
		// Cannot include any combination of matches.
		//
		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str7()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfSpades,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfClubs,
				GamingFixture.SevenOfDiamonds,
				GamingFixture.EightOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight7");
			expResults.Add("Flush5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str7_TheWheel()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfSpades,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfClubs,
				GamingFixture.SevenOfDiamonds,
				GamingFixture.AceOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight7");
			expResults.Add("Flush5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str7_StrFl5()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfDiamonds,
				GamingFixture.SevenOfClubs,
				GamingFixture.EightOfHearts,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight7");
			expResults.Add("StraightFlush5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str7_TheWheel_StrFl5()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfHearts,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfDiamonds,
				GamingFixture.SevenOfDiamonds,
				GamingFixture.AceOfClubs,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight7");
			expResults.Add("StraightFlush5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		//
		// Base Case: 5 suited cards
		// 6-card constraint: Straight6
		// Can include the following additional hands:
		// - StraightFlush5Pair
		// - StraightFlush5
		// - Flush5Pair
		// - Flush5 (implied by the Base scenario)
		//
		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str6()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfSpades,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfClubs,
				GamingFixture.SevenOfDiamonds,
				GamingFixture.NineOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight6");
			expResults.Add("Flush5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str6_TheWheel()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfSpades,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfClubs,
				GamingFixture.EightOfDiamonds,
				GamingFixture.AceOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight6");
			expResults.Add("Flush5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str6_StrFl5Pair()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfDiamonds,
				GamingFixture.SevenOfHearts,
				GamingFixture.SevenOfSpades,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("StraightFlush5Pair");
			expResults.Add("Straight6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str6_TheWheel_StrFl5Pair()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfDiamonds,
				GamingFixture.AceOfClubs,
				GamingFixture.AceOfSpades,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("StraightFlush5Pair");
			expResults.Add("Straight6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str6_StrFl5Pair_TheWheel()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfSpades,
				GamingFixture.SixOfClubs,
				GamingFixture.AceOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("StraightFlush5Pair");
			expResults.Add("Straight6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str6_StrFl5()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfDiamonds,
				GamingFixture.SevenOfHearts,
				GamingFixture.NineOfSpades,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight6");
			expResults.Add("StraightFlush5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str6_TheWheel_StrFl5()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfDiamonds,
				GamingFixture.AceOfClubs,
				GamingFixture.KingOfSpades,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight6");
			expResults.Add("StraightFlush5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str6_StrFl5_TheWheel()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfSpades,
				GamingFixture.EightOfClubs,
				GamingFixture.AceOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight6");
			expResults.Add("StraightFlush5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str6_Fl5Pair()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfClubs,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfDiamonds,
				GamingFixture.SevenOfDiamonds,
				GamingFixture.FourOfSpades,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush5Pair");
			expResults.Add("Straight6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str6_TheWheel_Fl5Pair()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfHearts,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfDiamonds,
				GamingFixture.FourOfClubs,
				GamingFixture.AceOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush5Pair");
			expResults.Add("Straight6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		//
		// Base Case: 5 suited cards
		// 5-card constraint: Straight5
		// Include the following hands:
		// - StraightFlush5Pair
		// - StraightFlush5
		// - Flush5Pair
		// - Straight5Pair
		// - Flush5(implied by the Base Case)
		// - Straight5 (implied by the Constraint)
		// Cannot include the following hands:
		//
		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str5_StrFl5Pair()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfDiamonds,
				GamingFixture.EightOfHearts,
				GamingFixture.EightOfSpades,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("StraightFlush5Pair");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str5_TheWheel_StrFl5Pair()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SevenOfHearts,
				GamingFixture.SevenOfClubs,
				GamingFixture.AceOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("StraightFlush5Pair");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str5_StrFl5()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfDiamonds,
				GamingFixture.EightOfHearts,
				GamingFixture.JackOfSpades,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("StraightFlush5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str5_TheWheel_StrFl5()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfDiamonds,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SevenOfHearts,
				GamingFixture.QueenOfClubs,
				GamingFixture.AceOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("StraightFlush5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str5_Fl5Pair()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfClubs,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfDiamonds,
				GamingFixture.FourOfHearts,
				GamingFixture.EightOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush5Pair");
			expResults.Add("Straight5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str5_TheWheel_Fl5Pair()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfClubs,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.TenOfDiamonds,
				GamingFixture.FourOfHearts,
				GamingFixture.AceOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush5Pair");
			expResults.Add("Straight5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str5Pair_Fl5()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfClubs,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfDiamonds,
				GamingFixture.EightOfHearts,
				GamingFixture.EightOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush5");
			expResults.Add("Straight5Pair");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str5Pair_TheWheel_Fl5()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfClubs,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.AceOfDiamonds,
				GamingFixture.EightOfHearts,
				GamingFixture.EightOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush5");
			expResults.Add("Straight5Pair");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str5_Fl5()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfClubs,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.SixOfDiamonds,
				GamingFixture.EightOfHearts,
				GamingFixture.TenOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush5");
			expResults.Add("Straight5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "5Suited")]
		public void FindFlushesAndStraights_5SuitedCards_Str5_TheWheel_Fl5()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfDiamonds,
				GamingFixture.ThreeOfDiamonds,
				GamingFixture.FourOfClubs,
				GamingFixture.FiveOfDiamonds,
				GamingFixture.AceOfDiamonds,
				GamingFixture.EightOfHearts,
				GamingFixture.TenOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Flush5");
			expResults.Add("Straight5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}
		#endregion // Base: 5 suited cards

		#region Base: 7 Card Run
		//
		// Do not include flushes because straight+flush combos are covered by other tests.
		// Can include the following hands:
		// - Straight7 (implied by the Base scenario)
		//
		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_7CardRun_Str7()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.AceOfSpades,
				GamingFixture.EightOfClubs,
				GamingFixture.NineOfHearts,
				GamingFixture.JackOfSpades,
				GamingFixture.KingOfSpades,
				GamingFixture.QueenOfClubs,
				GamingFixture.TenOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight7");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_7CardRun_Str7_TheWheel()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.AceOfSpades,
				GamingFixture.TwoOfClubs,
				GamingFixture.ThreeOfHearts,
				GamingFixture.FourOfSpades,
				GamingFixture.FiveOfSpades,
				GamingFixture.SixOfClubs,
				GamingFixture.SevenOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight7");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}
		#endregion


		#region Base: 6 Card Run
		//
		// Do not include flushes because straight+flush combos are covered by other tests.
		// Can include the following hands:
		// - Straight5Pair
		// - Straight6 (implied by the Base scenario)
		//
		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6CardRun_Str5Pair()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.AceOfSpades,
				GamingFixture.AceOfClubs,
				GamingFixture.NineOfHearts,
				GamingFixture.JackOfSpades,
				GamingFixture.KingOfSpades,
				GamingFixture.QueenOfClubs,
				GamingFixture.TenOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight5Pair");
			expResults.Add("Straight6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6CardRun_Str5Pair_TheWheel()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.AceOfSpades,
				GamingFixture.TwoOfClubs,
				GamingFixture.ThreeOfHearts,
				GamingFixture.FourOfSpades,
				GamingFixture.FiveOfSpades,
				GamingFixture.SixOfClubs,
				GamingFixture.SixOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight5Pair");
			expResults.Add("Straight6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6CardRun_Str6()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.AceOfSpades,
				GamingFixture.TwoOfClubs,
				GamingFixture.NineOfHearts,
				GamingFixture.JackOfSpades,
				GamingFixture.KingOfSpades,
				GamingFixture.QueenOfClubs,
				GamingFixture.TenOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6CardRun_Str6_TheWheel()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.AceOfSpades,
				GamingFixture.TwoOfClubs,
				GamingFixture.ThreeOfHearts,
				GamingFixture.FourOfSpades,
				GamingFixture.FiveOfSpades,
				GamingFixture.SixOfClubs,
				GamingFixture.EightOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight6");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}
		#endregion

		#region Base: 5 Card Run
		//
		// Do not include flushes because straight+flush combos are covered by other tests.
		// Can include the following hands:
		// - Straight5Pair
		// - Straight5 (implied by the Base scenario)
		//
		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_5CardRun_Str5Pair()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.AceOfSpades,
				GamingFixture.AceOfClubs,
				GamingFixture.AceOfHearts,
				GamingFixture.JackOfSpades,
				GamingFixture.KingOfSpades,
				GamingFixture.QueenOfClubs,
				GamingFixture.TenOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight5Pair");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_5CardRun_Str5Pair_TheWheel()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.AceOfSpades,
				GamingFixture.TwoOfClubs,
				GamingFixture.ThreeOfHearts,
				GamingFixture.FourOfSpades,
				GamingFixture.FiveOfSpades,
				GamingFixture.AceOfClubs,
				GamingFixture.AceOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight5Pair");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_5CardRun_Str5()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.AceOfSpades,
				GamingFixture.TwoOfClubs,
				GamingFixture.EightOfHearts,
				GamingFixture.JackOfSpades,
				GamingFixture.KingOfSpades,
				GamingFixture.QueenOfClubs,
				GamingFixture.TenOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_6CardRun_Str5_TheWheel()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.AceOfSpades,
				GamingFixture.TwoOfClubs,
				GamingFixture.ThreeOfHearts,
				GamingFixture.FourOfSpades,
				GamingFixture.FiveOfSpades,
				GamingFixture.SevenOfClubs,
				GamingFixture.EightOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Straight5");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}
		#endregion

		#region Matches
		//
		// Do not include flushes or straights because those are covered in other tests.
		// Can include the following hands:
		// - QuadSet
		// - SetPairPair
		// - QuadPair
		// - SetSet
		// - PairPairPair
		// - SetPair
		// - Quad
		// - PairPair
		// - Set
		// - Pair
		//
		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_Matches_QuadSet()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.AceOfSpades,
				GamingFixture.AceOfClubs,
				GamingFixture.AceOfHearts,
				GamingFixture.JackOfSpades,
				GamingFixture.AceOfDiamonds,
				GamingFixture.JackOfClubs,
				GamingFixture.JackOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("QuadSet");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_Matches_SetPairPair()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.ThreeOfSpades,
				GamingFixture.AceOfClubs,
				GamingFixture.ThreeOfHearts,
				GamingFixture.JackOfSpades,
				GamingFixture.AceOfSpades,
				GamingFixture.JackOfClubs,
				GamingFixture.JackOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("SetPairPair");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_Matches_QuadPair()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.ThreeOfSpades,
				GamingFixture.JackOfHearts,
				GamingFixture.ThreeOfHearts,
				GamingFixture.JackOfSpades,
				GamingFixture.AceOfSpades,
				GamingFixture.JackOfClubs,
				GamingFixture.JackOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("QuadPair");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_Matches_SetSet()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.ThreeOfSpades,
				GamingFixture.JackOfHearts,
				GamingFixture.ThreeOfHearts,
				GamingFixture.JackOfSpades,
				GamingFixture.AceOfSpades,
				GamingFixture.ThreeOfClubs,
				GamingFixture.JackOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("SetSet");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_Matches_PairPairPair()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.ThreeOfSpades,
				GamingFixture.JackOfClubs,
				GamingFixture.ThreeOfHearts,
				GamingFixture.JackOfSpades,
				GamingFixture.AceOfSpades,
				GamingFixture.AceOfClubs,
				GamingFixture.FiveOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("PairPairPair");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_Matches_SetPair()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.ThreeOfSpades,
				GamingFixture.JackOfClubs,
				GamingFixture.ThreeOfHearts,
				GamingFixture.SixOfSpades,
				GamingFixture.AceOfSpades,
				GamingFixture.JackOfHearts,
				GamingFixture.JackOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("SetPair");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_Matches_Quad()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.ThreeOfSpades,
				GamingFixture.JackOfHearts,
				GamingFixture.FourOfHearts,
				GamingFixture.JackOfSpades,
				GamingFixture.AceOfSpades,
				GamingFixture.JackOfClubs,
				GamingFixture.JackOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Quad");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_Matches_PairPair()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.ThreeOfSpades,
				GamingFixture.QueenOfClubs,
				GamingFixture.ThreeOfHearts,
				GamingFixture.JackOfSpades,
				GamingFixture.AceOfSpades,
				GamingFixture.JackOfClubs,
				GamingFixture.FourOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("PairPair");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_Matches_Set()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.ThreeOfSpades,
				GamingFixture.FourOfClubs,
				GamingFixture.TwoOfHearts,
				GamingFixture.FourOfSpades,
				GamingFixture.AceOfSpades,
				GamingFixture.NineOfClubs,
				GamingFixture.FourOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Set");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_Matches_Pair()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.ThreeOfSpades,
				GamingFixture.TwoOfClubs,
				GamingFixture.ThreeOfHearts,
				GamingFixture.JackOfSpades,
				GamingFixture.AceOfSpades,
				GamingFixture.SixOfClubs,
				GamingFixture.TenOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("Pair");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}
		#endregion

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_HighCard_Highest()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.AceOfSpades,
				GamingFixture.KingOfClubs,
				GamingFixture.QueenOfHearts,
				GamingFixture.JackOfSpades,
				GamingFixture.NineOfSpades,
				GamingFixture.EightOfClubs,
				GamingFixture.SevenOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("HighCard");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}

		[Fact]
		[Trait("7CardMashup", "POC")]
		public void FindFlushesAndStraights_HighCard_Lowest()
		{
			List<Card> hand = new List<Card>
			{
				GamingFixture.TwoOfSpades,
				GamingFixture.ThreeOfClubs,
				GamingFixture.FourOfHearts,
				GamingFixture.FiveOfSpades,
				GamingFixture.SevenOfSpades,
				GamingFixture.EightOfClubs,
				GamingFixture.NineOfDiamonds,
			};
			hand.Sort();
			hand.Reverse();

			List<string> rankings = PokerGame.PocNewMethodFor7Cards(hand);
			foreach (string r in rankings)
				Output.WriteLine($"{r}");

			List<string> expResults = new List<string>();
			expResults.Add("HighCard");

			Assert.Equal(expResults.Count, rankings.Count);
			foreach (string expRes in expResults)
				Assert.Contains(expRes, rankings);
		}
	}
}
