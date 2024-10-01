using System;
using System.Collections;
using System.Linq;
using BattleForTheCastle.Cards;

namespace BattleForTheCastle.Game
{
	public enum BattleResults
	{
		Player1Win,
		Player2Win,
		Draw
	}
	public class Battle
	{
		private Player Player1 { get; set; }

		private Player Player2 { get; set; }

		private ElementType Zone { get; set; }
		private Card LockedCard { get; set; }

		public BattleResults Results { get; set; }

		public Battle(Player player1, Player player2, ElementType zone)
		{
			Player1 = player1;
			Player2 = player2;
			Zone = zone;
		}

		void PickCards()
		{
			Player1.PickCard();
			Player2.PickCard();
		}

		private void Activation(MonsterCard monsterCard1, MonsterCard monsterCard2)
		{
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
        }

		private void Reveal()
		{
			var card1 = Player1.PickedCard;
			var card2 = Player2.PickedCard;
            Player1.StackCard();
            Player2.StackCard();

            switch (card1)
			{
				case MonsterCard monsterCard1 when card2 is MonsterCard monsterCard2:
					Console.WriteLine("Picked cards are: " + monsterCard1.Name + " (" + monsterCard1.Attack +  ")" + " and " + monsterCard2.Name + " (" + monsterCard2.Attack + ")");
					Activation(monsterCard1, monsterCard2);

					if (monsterCard1.Attack > monsterCard2.Attack)
					{
						Console.WriteLine("P1 wins.");
						Player1.LockCard();
						Player2.UnlockCard();
						Player1.EmptyStack();
                        Player2.EmptyStackGivingNeutrals(Player1);
                    }
					else if (monsterCard1.Attack < monsterCard2.Attack)
					{
						Console.WriteLine("P2 wins.");
						Player1.UnlockCard();
						Player2.LockCard();
						Player1.EmptyStackGivingNeutrals(Player2);
						Player2.EmptyStack();
					}
					else
					{
						Console.WriteLine("Draw...");
						Player1.UnlockCard();
						Player2.UnlockCard();
					}

					break;
				case MonsterCard monsterCard1 when card2 is MagicCard magicCard2:
					Console.WriteLine("Picked cards are: " + monsterCard1.Name + " and " + magicCard2.Name);
					switch (monsterCard1)
					{
						case NeutralCard neutralCard1:
							neutralCard1.Activate();
							break;
						case FamilyCard familyCard1:
							familyCard1.Family.Ability();
							break;
					}
					break;
				case MagicCard magicCard1 when card2 is MonsterCard monsterCard2:
					Console.WriteLine("Picked cards are: " + magicCard1.Name + " and " + monsterCard2.Name);
					break;
				case MagicCard magicCard1 when card2 is MagicCard magicCard2:
					Console.WriteLine("Picked cards are: " + magicCard1.Name + " and " + magicCard2.Name);
					break;
			}
		}

		private void EmptyStacks()
		{
			Player1.EmptyStack();
            Player2.EmptyStack();
        }

		private bool IsBattleOver()
		{
			bool isPlayer1Locked = !Player1.CanPlay();
			bool isPlayer2Locked = !Player2.CanPlay();

			return isPlayer1Locked || isPlayer2Locked;
		}

		private BattleResults ApplyResults()
		{
			if (Player1.Board.Army.Count == 0 && Player2.Board.Army.Count == 0)
			{
				EmptyStacks();
				return BattleResults.Draw;
			}

			if (Player1.Board.Army.Count == 0 || !Player1.CanPlay())
			{
				return BattleResults.Player2Win;
			}

			if (Player2.Board.Army.Count == 0 || !Player2.CanPlay())
			{
				return BattleResults.Player1Win;
			}

			return BattleResults.Draw;
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
					Results = ApplyResults();
					continue;
				}
				PickCards();
				Reveal();
				Status();
			}
		}

		void Status()
		{
			Console.WriteLine("\n============= Status ==============");
			Console.WriteLine("Player 1 army:");
			foreach (var card in Player1.Board.Army)
			{
				Console.WriteLine(card.Name);
			}

			Console.WriteLine("\nPlayer 1 Recovery:");
			foreach (var card in Player1.Board.Recovery)
			{
				Console.WriteLine(card.Name);
			}

			Console.WriteLine("\nPlayer 2 army:");
			foreach (var card in Player2.Board.Army)
			{
				Console.WriteLine(card.Name);
			}

			Console.WriteLine("\nPlayer 2 Recovery:");
			foreach (var card in Player2.Board.Recovery)
			{
				Console.WriteLine(card.Name);
			}
			Console.WriteLine("\n===========================\n");
		}
	}
}