using System;
using System.Collections;
using System.Linq;
using BattleForTheCastle.Card;

namespace BattleForTheCastle.Battle
{
	public enum BattleResults
	{
		Player1Win,
		Player2Win,
		Draw
	}
	public class Battle
	{
		private Board.Board Board1 { get; set; }
		private Board.Board Board2 { get; set; }
		private ElementType Zone { get; set; }
		private Card.Card LockedCard { get; set; }
		private Card.Card P1PickedCard { get; set; }
		private Card.Card P2PickedCard { get; set; }
		private Stack P1BattleStack { get; set; }
		private Stack P2BattleStack { get; set; }
		public BattleResults Results { get; set; }

		public Battle(Board.Board board1, Board.Board board2, ElementType zone)
		{
			Board1 = board1;
			Board2 = board2;
			P1BattleStack = new Stack();
			P2BattleStack = new Stack();
			Zone = zone;
		}

		void PickCard()
		{
			var rand = new Random();
			Console.WriteLine("Player 1 picking...");
			var playableCards1 = Board1.Army.Where(card => card != LockedCard).ToList();
			P1PickedCard = playableCards1[rand.Next(0, playableCards1.Count)];
			Console.WriteLine("Player 2 picking...");
			var playableCards2 = Board2.Army.Where(card => card != LockedCard).ToList();
			P2PickedCard = playableCards2[rand.Next(0, playableCards2.Count)];
		}

		void Reveal()
		{
			var card1 = P1PickedCard;
			var card2 = P2PickedCard;

			if (card1 is MonsterCard monsterCard1 && card2 is MonsterCard monsterCard2)
			{
				Console.WriteLine("Picked cards are: " + monsterCard1.Name + " (" + monsterCard1.Stats["ATK"] +  ")" + " and " + monsterCard2.Name + " (" + monsterCard2.Stats["ATK"] + ")");
				switch (monsterCard1)
				{
					case NeutralCard neutralCard1:
						neutralCard1.Activate();
						break;
					case FamilyCard familyCard1:
						familyCard1.Family.Ability();
						break;
				}

				switch (monsterCard2)
				{
					case NeutralCard neutralCard2:
						neutralCard2.Activate();
						break;
					case FamilyCard familyCard2:
						familyCard2.Family.Ability();
						break;
				}

				if (monsterCard1.Stats["ATK"] > monsterCard2.Stats["ATK"])
				{
					Console.WriteLine("P1 wins.");
					LockedCard = P1PickedCard;
					Board2.Army.Remove(monsterCard2);
					switch (monsterCard2)
					{
						case NeutralCard neutralCard2:
							Board1.Recovery.Add(neutralCard2);
							break;
						case FamilyCard familyCard2:
							Board2.Recovery.Add(familyCard2);
							break;
					}

					EmptyStackForP1();
				}
				else if (monsterCard1.Stats["ATK"] < monsterCard2.Stats["ATK"])
				{
					Console.WriteLine("P2 wins.");
					LockedCard = P2PickedCard;
					Board1.Army.Remove(monsterCard1);
					switch (monsterCard1)
					{
						case NeutralCard neutralCard1:
							Board2.Recovery.Add(neutralCard1);
							break;
						case FamilyCard familyCard1:
							Board1.Recovery.Add(familyCard1);
							break;
					}

					EmptyStackForP2();
				}
				else
				{
					Console.WriteLine("Draw...");
					LockedCard = null;
					P1BattleStack.Push(P1PickedCard);
					Board1.Army.Remove(monsterCard1);
					P2BattleStack.Push(P2PickedCard);
					Board2.Army.Remove(monsterCard2);
				}
			}
			else if (card1 is MonsterCard && card2 is MagicCard)
			{

			}
			else if (card1 is MagicCard && card2 is MonsterCard)
			{
				
			}
			else if (card1 is MagicCard && card2 is MagicCard)
			{

			}
		}

		void EmptyStack()
		{
			bool isFirst = true;
			while (P1BattleStack.Count > 0)
			{
				var card = (Card.Card) P1BattleStack.Pop();
				if (isFirst)
				{
					Board1.Army.Add(card);
					isFirst = false;
					continue;
				}
				switch (card)
				{
					case MonsterCard monsterCard:
						Board1.Recovery.Add(monsterCard);
						break;
					case MagicCard magicCard:
						Board1.UsedMagicZone.Add(magicCard);
						break;
				}
			}

			isFirst = true;
			while (P2BattleStack.Count > 0)
			{
				var card = (Card.Card) P2BattleStack.Pop();
				if (isFirst)
				{
					Board2.Army.Add(card);
					isFirst = false;
					continue;
				}
				switch (card)
				{
					case MonsterCard monsterCard:
						Board2.Recovery.Add(monsterCard);
						break;
					case MagicCard magicCard:
						Board2.UsedMagicZone.Add(magicCard);
						break;
				}
			}
		}
		void EmptyStackForP1()
		{
			while (P1BattleStack.Count > 0)
			{
				var card = (Card.Card) P1BattleStack.Pop();
				switch (card)
				{
					case MonsterCard monsterCard:
						Board1.Recovery.Add(monsterCard);
						break;
					case MagicCard magicCard:
						Board1.UsedMagicZone.Add(magicCard);
						break;
				}
			}

			while (P2BattleStack.Count > 0)
			{
				var card = (Card.Card)P2BattleStack.Pop();
				switch (card)
				{
					case NeutralCard neutralCard:
						Board1.Recovery.Add(neutralCard);
						break;
					case FamilyCard familyCard:
						Board2.Recovery.Add(familyCard);
						break;
					case MagicCard magicCard:
						Board2.UsedMagicZone.Add(magicCard);
						break;
				}
			}
		}
		void EmptyStackForP2()
		{
			while (P1BattleStack.Count > 0)
			{
				var card = (Card.Card)P1BattleStack.Pop();
				switch (card)
				{
					case NeutralCard neutralCard:
						Board2.Recovery.Add(neutralCard);
						break;
					case FamilyCard familyCard:
						Board1.Recovery.Add(familyCard);
						break;
					case MagicCard magicCard:
						Board1.UsedMagicZone.Add(magicCard);
						break;
				}
			}

			while (P2BattleStack.Count > 0)
			{
				var card = (Card.Card)P2BattleStack.Pop();
				switch (card)
				{
					case MonsterCard monsterCard:
						Board2.Recovery.Add(monsterCard);
						break;
					case MagicCard magicCard:
						Board2.UsedMagicZone.Add(magicCard);
						break;
				}
			}
		}

		bool IsBattleOver()
		{
			bool isPlayer1Locked = false;
			bool isPlayer2Locked = false;
			if (Board1.Army.Count <= 1)
			{
				if (Board1.Army.Contains(LockedCard) || Board1.Army.Count == 0)
				{
					isPlayer1Locked = true;
				}
			}
			if (Board2.Army.Count <= 1)
			{
				if (Board2.Army.Contains(LockedCard) || Board2.Army.Count == 0)
				{
					isPlayer2Locked = true;
				}
			}

			return isPlayer1Locked || isPlayer2Locked;
		}

		void ApplyResults()
		{
			if (Board1.Army.Count == 0 && Board2.Army.Count == 0)
			{
				EmptyStack();
				Results = BattleResults.Draw;
			}
			if (Board1.Army.Count == 0)
			{
				if (P1BattleStack.Count > 0)
				{
					var card = (Card.Card)P1BattleStack.Pop();
					Board1.Army.Add(card);
					EmptyStackForP2();
				}

				Results = BattleResults.Player2Win;
			}

			if (Board2.Army.Count == 0)
			{
				if (P2BattleStack.Count > 0)
				{
					var card = (Card.Card)P2BattleStack.Pop();
					Board2.Army.Add(card);
					EmptyStackForP1();
				}

				Results = BattleResults.Player1Win;
			}
		}

		public void Start()
		{
			Console.WriteLine("Battle starts!");

			bool isBattleOver = false;

			while (!isBattleOver)
			{
				isBattleOver = IsBattleOver();
				if (isBattleOver)
				{
					ApplyResults();
					continue;
				}
				PickCard();
				Reveal();
				Status();
			}
		}

		void Status()
		{
			Console.WriteLine("\n============= Status ==============");
			Console.WriteLine("Player 1 army:");
			foreach (var card in Board1.Army)
			{
				Console.WriteLine(card.Name);
			}

			Console.WriteLine("\nPlayer 1 Recovery:");
			foreach (var card in Board1.Recovery)
			{
				Console.WriteLine(card.Name);
			}

			Console.WriteLine("\nPlayer 2 army:");
			foreach (var card in Board2.Army)
			{
				Console.WriteLine(card.Name);
			}

			Console.WriteLine("\nPlayer 2 Recovery:");
			foreach (var card in Board2.Recovery)
			{
				Console.WriteLine(card.Name);
			}
			Console.WriteLine("\n===========================\n");
		}
	}
}