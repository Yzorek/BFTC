using System.Collections.Generic;
using BattleForTheCastle.Card;

namespace BattleForTheCastle.Board
{
	public class Board
	{
		public List<Card.Card> Army { get; set; }
		public List<Card.Card> Deck { get; set; }
		public List<MonsterCard> Recovery { get; set; }
		public List<MagicCard> UsedMagicZone { get; set; }
		public int FoodCount { get; set; }

		public Board()
		{
			Army = new List<Card.Card>();
			Deck = new List<Card.Card>();
			Recovery = new List<MonsterCard>();
			UsedMagicZone = new List<MagicCard>();
			FoodCount = 0;
		}
	}
}