using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleSamples.Casino_Sample
{
	public class Card : IComparable
	{
		public string Suit { get; private set; }
		public string Rank { get; private set; }
		public int RankValue
		{
			get
			{
				return GetRankValue();
			}
		}
		public override string ToString()
		{
			return Rank + Suit;
		}

		// IComparable
		public int CompareTo(object obj)
		{
			if (obj is Card)
			{
				if (GetRankValue() < (obj as Card).GetRankValue())
					return -1;
				else if (GetRankValue() > (obj as Card).GetRankValue())
					return 1;
				else
					return 0;
			}
			throw new ArgumentException("Can't compare a Card to a non-Card.");
		}

		public Card() : this("A", "s") { }

		public Card(string rank, string suit)
		{
			switch (suit.ToLower())
			{
				case "s":
				case "h":
				case "d":
				case "c":
					break;
				default:
					throw new ArgumentException();
			}

			switch (rank.ToLower())
			{
				case "a":
				case "k":
				case "q":
				case "j":
				case "t":
				case "9":
				case "8":
				case "7":
				case "6":
				case "5":
				case "4":
				case "3":
				case "2":
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			Suit = suit.ToLower();
			Rank = rank.ToUpper();
		}

		int GetRankValue()
		{
			switch (Rank)
			{
				case "A":
					return 14; // 0xE
				case "K":
					return 13; // 0xD
				case "Q":
					return 12; // 0xC
				case "J":
					return 11; // 0xB
				case "T":
					return 10; // 0xA
				case "9":
					return 9;
				case "8":
					return 8;
				case "7":
					return 7;
				case "6":
					return 6;
				case "5":
					return 5;
				case "4":
					return 4;
				case "3":
					return 3;
				case "2":
					return 2;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}
