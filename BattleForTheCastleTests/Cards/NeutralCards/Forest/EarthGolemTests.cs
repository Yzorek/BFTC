using BattleForTheCastle.Cards.NeutralCards.Undead;
using BattleForTheCastle.Game;
using BattleForTheCastleTests;

namespace BattleForTheCastle.Cards.NeutralCards.Forest.Tests
{
    [TestClass()]
    public class EarthGolemTests
    {
        [TestMethod()]
        public void ActivateBeforeRevealTest_VersusOtherThanAir()
        {
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");

            player1.Board.Deck.Add(new EarthGolem(player1.Name));
            BoardBuilder.InitBoard("Dragon azur", player2);

            BoardBuilder.BuildArmy(player1.Board);
            BoardBuilder.BuildArmy(player2.Board);

            player1.PickCard(0);
            player1.StackCard();
            player2.PickCard(0);
            player2.StackCard();

            ((IActivableBattleBeforeReveal)player1.PickedCard).ActivateBeforeReveal([player1, player2], new Battle(player1, player2, ElementType.Fire), player2);

            Assert.IsTrue(((MonsterCard)player2.PickedCard).EffectiveAttack == 15, "L'attaque effective devrait être de 15.");
        }

        [TestMethod()]
        public void ActivateBeforeRevealTest_VersusAir()
        {
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");

            player1.Board.Deck.Add(new EarthGolem(player1.Name));
            BoardBuilder.InitBoard("L'invisible", player2);

            BoardBuilder.BuildArmy(player1.Board);
            BoardBuilder.BuildArmy(player2.Board);

            player1.PickCard(0);
            player1.StackCard();
            player2.PickCard(0);
            player2.StackCard();

            ((IActivableBattleBeforeReveal)player1.PickedCard).ActivateBeforeReveal([player1, player2], new Battle(player1, player2, ElementType.Fire), player2);

            Assert.IsTrue(((MonsterCard)player2.PickedCard).EffectiveAttack == 7, "L'attaque effective devrait être de 7.");
        }
    }
}