using System.Collections.Generic;
using BattleForTheCastle.Cards;

namespace BattleForTheCastle.Board
{
	public class PlayerBoard
	{
		public List<Card> Army { get; set; }
		public List<Card> Deck { get; set; }
		public List<MonsterCard> Recovery { get; set; }
		public List<MagicCard> UsedMagicZone { get; set; }
		public int FoodCount { get; set; }

		public PlayerBoard()
		{
			Army = new List<Card>();
			Deck = new List<Card>();
			Recovery = new List<MonsterCard>();
			UsedMagicZone = new List<MagicCard>();
			FoodCount = 0;
		}
	}
}