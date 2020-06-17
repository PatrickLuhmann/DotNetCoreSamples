using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleSamples.Casino_Sample
{
	public class Deck
	{
		public List<Card> Cards;

		public Card Deal()
		{
			Card theCard = Cards[0];
			Cards.RemoveAt(0);
			return theCard;
		}

		/// <summary>
		/// Returns the given cards to the bottom of the deck.
		/// </summary>
		/// <param name="cards">The cards to return.</param>
		public void Return(Card[] cards)
		{
			foreach (Card card in cards)
			{
				this.Cards.Add(card);
			}
		}

		/// <summary>
		/// Returns the given cards to the bottom of the deck.
		/// </summary>
		/// <param name="cards">The cards to return.</param>
		public void Return(List<Card> cards)
		{
			foreach (Card card in cards)
			{
				this.Cards.Add(card);
			}
		}

		public void Shuffle()
		{
			List<Card> target = new List<Card>();

			Random rng = new Random();
			while (Cards.Count > 0)
			{
				int rand = rng.Next(Cards.Count);
				Card selectedCard = Cards[rand];
				target.Add(selectedCard);
				Cards.RemoveAt(rand);
			}

			Cards = target;
		}

		public Deck()
		{
			Cards = new List<Card>();

			// Create a standard 52-card deck.
			// Order is Spades then Diamonds (A - K), then
			// Clubs then Hearts (K - A).
			string suit;

			// Spades
			suit = "s";
			//			Cards.Add(new Card("A", suit));
			Cards.Add(new Card()); // default card is the Ace Of Spades.
			for (int x = 2; x < 10; x++)
			{
				Cards.Add(new Card(x.ToString(), suit));
			}
			Cards.Add(new Card("T", suit));
			Cards.Add(new Card("J", suit));
			Cards.Add(new Card("Q", suit));
			Cards.Add(new Card("K", suit));

			// Diamonds
			suit = "d";
			Cards.Add(new Card("A", suit));
			for (int x = 2; x < 10; x++)
			{
				Cards.Add(new Card(x.ToString(), suit));
			}
			Cards.Add(new Card("T", suit));
			Cards.Add(new Card("J", suit));
			Cards.Add(new Card("Q", suit));
			Cards.Add(new Card("K", suit));

			// Clubs
			suit = "c";
			Cards.Add(new Card("K", suit));
			Cards.Add(new Card("Q", suit));
			Cards.Add(new Card("J", suit));
			Cards.Add(new Card("T", suit));
			for (int x = 9; x > 1; x--)
			{
				Cards.Add(new Card(x.ToString(), suit));
			}
			Cards.Add(new Card("A", suit));

			// Hearts
			suit = "h";
			Cards.Add(new Card("K", suit));
			Cards.Add(new Card("Q", suit));
			Cards.Add(new Card("J", suit));
			Cards.Add(new Card("T", suit));
			for (int x = 9; x > 1; x--)
			{
				Cards.Add(new Card(x.ToString(), suit));
			}
			Cards.Add(new Card("A", suit));
		}
	}
}
