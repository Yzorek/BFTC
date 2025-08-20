using BattleForTheCastle.Game;
using BattleForTheCastleTests;

namespace BattleForTheCastle.Cards.NeutralCards.Undead.Tests
{
    [TestClass()]
    public class SkeletonSoldierTests
    {
        [TestMethod()]
        public void ActivateTest_VersusOtherThanFairie()
        {
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");

            player1.Board.Deck.Add(new SkeletonSoldier(player1.Name));

            BoardBuilder.InitBoard(new List<string>()
            {
                "L'invisible"
            }, player2);

            BoardBuilder.BuildArmy(player1.Board);
            BoardBuilder.BuildArmy(player2.Board);

            player1.PickCard(0);
            player1.StackCard();
            player2.PickCard(0);
            player2.StackCard();

            ((IActivableBattleBeforeReveal)player1.PickedCard).ActivateBeforeReveal([player1, player2], new Battle(player1, player2, ElementType.Fire), player2);

            Assert.IsTrue(((MonsterCard)player1.PickedCard).EffectiveAttack == 9, "L'attaque effective devrait être de 9.");
        }

        [TestMethod()]
        public void ActivateTest_VersusFairie()
        {
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");

            player1.Board.Deck.Add(new SkeletonSoldier(player1.Name));

            BoardBuilder.InitBoard(new List<string>()
            {
                "Fée 3"
            }, player2);

            BoardBuilder.BuildArmy(player1.Board);
            BoardBuilder.BuildArmy(player2.Board);

            player1.PickCard(0);
            player1.StackCard();
            player2.PickCard(0);
            player2.StackCard();

            ((IActivableBattleBeforeReveal)player1.PickedCard).ActivateBeforeReveal([player1, player2], new Battle(player1, player2, ElementType.Fire), player2);

            Assert.IsTrue(((MonsterCard) player1.PickedCard).EffectiveAttack == 13, "L'attaque effective devrait être de 13.");
        }
    }
}