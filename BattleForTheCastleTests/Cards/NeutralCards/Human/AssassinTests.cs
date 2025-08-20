using BattleForTheCastle.Game;
using BattleForTheCastleTests;

namespace BattleForTheCastle.Cards.NeutralCards.Human.Tests
{
    [TestClass()]
    public class AssassinTests
    {
        [TestMethod()]
        public void ActivateBeforeRevealTest_VersusOtherThanBubble()
        {
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");

            player1.Board.Deck.Add(new Assassin(player1.Name));
            BoardBuilder.InitBoard("L'invisible", player2);

            BoardBuilder.BuildArmy(player1.Board);
            BoardBuilder.BuildArmy(player2.Board);

            player1.PickCard(0);
            player1.StackCard();
            player2.PickCard(0);
            player2.StackCard();

            ((IActivableBattleBeforeReveal)player1.PickedCard).ActivateBeforeReveal([player1, player2], new Battle(player1, player2, ElementType.Fire), player2);

            Assert.IsTrue(((MonsterCard)player1.PickedCard).EffectiveAttack == 16, "L'attaque effective devrait être de 16.");
        }

        [TestMethod()]
        public void ActivateBeforeRevealTest_VersusBubble()
        {
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");

            player1.Board.Deck.Add(new Assassin(player1.Name));
            BoardBuilder.InitBoard("Mini bubble", player2);

            BoardBuilder.BuildArmy(player1.Board);
            BoardBuilder.BuildArmy(player2.Board);

            player1.PickCard(0);
            player1.StackCard();
            player2.PickCard(0);
            player2.StackCard();

            ((IActivableBattleBeforeReveal)player1.PickedCard).ActivateBeforeReveal([player1, player2], new Battle(player1, player2, ElementType.Fire), player2);

            Assert.IsTrue(((MonsterCard)player1.PickedCard).EffectiveAttack == 6, "L'attaque effective devrait être de 6.");
        }
    }
}