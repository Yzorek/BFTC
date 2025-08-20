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

		public BattleResults Results { get; set; }

		public Battle(Player player1, Player player2, ElementType zone)
		{
			Player1 = player1;
			Player2 = player2;
			Zone = zone;
		}

		void PickCards(Random random)
		{
            var playableCards1 = Player1.Board.Army.Where(card => card != Player1.LockedCard).ToList();
            Player1.PickCard(random.Next(0, playableCards1.Count));
            var playableCards2 = Player2.Board.Army.Where(card => card != Player2.LockedCard).ToList();
            Player2.PickCard(random.Next(0, playableCards2.Count));
		}

		private void BeforeRevealActivation(Card card)
		{
			if (card is IActivableBattleBeforeReveal activableCard)
				activableCard.ActivateBeforeReveal(new List<Player>()
				{
					Player1,
					Player2
				},
				this, Player2);
        }

        private void AfterLockActivation(Card card)
        {
            if (card is IActivableBattleAfterLocking activableCard)
                activableCard.ActivateAfterLocking(new List<Player>()
                {
                    Player1,
                    Player2
                },
                this, Player2);
        }

        private void PlayerWinResolves(Player playerWin, Player playerLoss)
		{
            playerWin.LockCard();
			AfterLockActivation(Player1.PickedCard);
			AfterLockActivation(Player2.PickedCard);
            playerLoss.UnlockCard();
            playerWin.EmptyStack(true, playerLoss);
            playerLoss.EmptyStack(false, playerWin);
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
					//Console.WriteLine("Picked cards are: " + monsterCard1.Name + " (" + monsterCard1.Attack +  ")" + " and " + monsterCard2.Name + " (" + monsterCard2.Attack + ")");
					BeforeRevealActivation(monsterCard1);
					BeforeRevealActivation(monsterCard2);

					if (monsterCard1.EffectiveAttack > monsterCard2.EffectiveAttack)
					{
						//Console.WriteLine("P1 wins.");
                        PlayerWinResolves(Player1, Player2);
                    }
					else if (monsterCard1.EffectiveAttack < monsterCard2.EffectiveAttack)
					{
						//Console.WriteLine("P2 wins.");
						PlayerWinResolves(Player2, Player1);
					}
					else
					{
						//Console.WriteLine("Draw...");
						Player1.UnlockCard();
						Player2.UnlockCard();
					}

					break;
				case MonsterCard monsterCard1 when card2 is MagicCard magicCard2:
					//Console.WriteLine("Picked cards are: " + monsterCard1.Name + " and " + magicCard2.Name);
					PlayerWinResolves(Player1, Player2);
                    break;
				case MagicCard magicCard1 when card2 is MonsterCard monsterCard2:
					//Console.WriteLine("Picked cards are: " + magicCard1.Name + " and " + monsterCard2.Name);
                    PlayerWinResolves(Player2, Player1);
                    break;
				case MagicCard magicCard1 when card2 is MagicCard magicCard2:
					//Console.WriteLine("Picked cards are: " + magicCard1.Name + " and " + magicCard2.Name);
					EmptyStacks();
					break;
			}
		}

		private void EmptyStacks()
		{
			Player1.EmptyStack(true, Player2);
            Player2.EmptyStack(true, Player1);
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

		public void Start(Random random)
		{
			//Console.WriteLine("Battle starts!");

			bool isBattleOver = false;

			while (!isBattleOver)
			{
				isBattleOver = IsBattleOver();
				if (isBattleOver)
				{
					Results = ApplyResults();
					continue;
				}
				PickCards(random);
				Reveal();
				//Status();
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