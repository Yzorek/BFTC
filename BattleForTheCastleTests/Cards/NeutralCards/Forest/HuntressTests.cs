using BattleForTheCastle.Game;
using BattleForTheCastleTests;

namespace BattleForTheCastle.Cards.NeutralCards.Forest.Tests
{
    [TestClass()]
    public class HuntressTests
    {
        [TestMethod()]
        public void ActivateBeforeRevealTest_FacingMagicCard()
        {
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");

            player1.Board.Deck.Add(new Huntress(player1.Name));
            player2.Board.Deck.Add(new MagicCard("Divination", "Au prochain tour, visionnez la carte de votre adversaire avant de choisir la votre."));

            BoardBuilder.BuildArmy(player1.Board);
            BoardBuilder.BuildArmy(player2.Board);

            player1.PickCard(0);
            player1.StackCard();
            player2.PickCard(0);
            player2.StackCard();

            ((IActivableBattleBeforeReveal)player1.PickedCard).ActivateBeforeReveal([player1, player2], new Battle(player1, player2, ElementType.Fire), player2);

            Assert.IsTrue(player2.PickedCard.IsDisabled, "La carte magie devrait être désactivée.");
        }

        [TestMethod()]
        public void ActivateBeforeRevealTest_FacingOtherThanMagicCard()
        {
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");

            player1.Board.Deck.Add(new Huntress(player1.Name));
            BoardBuilder.InitBoard(new List<string>
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

            Assert.IsFalse(player2.PickedCard.IsDisabled, "La carte ne devrait pas être désactivée.");
        }

        [TestMethod()]
        public void ActivateAfterLockingTest_FacingMagicCard()
        {
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");

            player1.Board.Deck.Add(new Huntress(player1.Name));
            player2.Board.Deck.Add(new MagicCard("Divination", "Au prochain tour, visionnez la carte de votre adversaire avant de choisir la votre."));

            BoardBuilder.BuildArmy(player1.Board);
            BoardBuilder.BuildArmy(player2.Board);

            player1.PickCard(0);
            player1.StackCard();
            player2.PickCard(0);
            player2.StackCard();
            player1.LockCard();

            ((IActivableBattleAfterLocking)player1.PickedCard).ActivateAfterLocking([player1, player2], new Battle(player1, player2, ElementType.Fire), player2);

            Assert.IsNull(player1.LockedCard, "La carte ne devrait pas être bloquée.");
        }

        [TestMethod()]
        public void ActivateAfterLockingTest_FacingOtherThanMagicCard()
        {
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");

            player1.Board.Deck.Add(new Huntress(player1.Name));
            BoardBuilder.InitBoard(new List<string>
            {
                "L'invisible"
            }, player2);

            BoardBuilder.BuildArmy(player1.Board);
            BoardBuilder.BuildArmy(player2.Board);

            player1.PickCard(0);
            player1.StackCard();
            player2.PickCard(0);
            player2.StackCard();
            player1.LockCard();

            ((IActivableBattleAfterLocking)player1.PickedCard).ActivateAfterLocking([player1, player2], new Battle(player1, player2, ElementType.Fire), player2);

            Assert.IsNotNull(player1.LockedCard, "La carte devrait être bloquée.");
        }
    }
}