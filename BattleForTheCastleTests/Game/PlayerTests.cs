using BattleForTheCastle.Cards.NeutralCards.Forest;
using BattleForTheCastleTests;

namespace BattleForTheCastle.Game.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        [TestMethod()]
        public void EmptyStackTest_BattleLoss()
        {
            Player player1 = new Player("Player 1");

            BoardBuilder.InitBoard(new List<string>()
            {
                "Ange 1",
                "Ange 2",
                "L'invisible"
            }, player1);

            BoardBuilder.BuildArmy(player1.Board);

            player1.PickCard(0);
            player1.StackCard();

            player1.PickCard(0);
            player1.StackCard();

            player1.PickCard(0);
            player1.StackCard();

            player1.EmptyStack(false, new Player("Player 2"));

            Assert.IsTrue(player1.Board.Recovery.Count == 3, "L'infirmerie devrait contenir 3 monstres.");
        }

        [TestMethod()]
        public void EmptyStackTest_BattleWon()
        {
            Player player2 = new Player("Player 2");

            BoardBuilder.InitBoard(new List<string>()
            {
                "Bébé dragon",
                "Jeune dragon",
                "Dragon azur"
            }, player2);

            BoardBuilder.BuildArmy(player2.Board);

            player2.PickCard(0);
            player2.StackCard();

            player2.PickCard(0);
            player2.StackCard();

            player2.PickCard(0);
            player2.StackCard();

            player2.EmptyStack(true, new Player("Player 1"));

            Assert.IsTrue(player2.Board.Army.Any(x => string.Equals(x.Name, "Dragon azur")), "L'armée devrait contenir un Dragon azur.");
        }

        [TestMethod()]
        public void EmptyStackTest_BattleLossWithNeutrals()
        {
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");
            player1.Board.Deck.Add(new Huntress(player1.Name));
            BoardBuilder.BuildArmy(player1.Board);

            Assert.IsTrue(player1.Board.Army[0].OwnerName == player1.Name, "Le propriétaire devrait être le joueur 1.");

            player1.PickCard(0);
            player1.StackCard();

            player1.EmptyStack(false, player2);

            Assert.IsTrue(player1.Board.Recovery.Count == 0, "L'infirmerie devrait contenir 0 monstre.");
            Assert.IsTrue(player2.Board.Recovery.Count == 1, "L'infirmerie devrait contenir 1 monstre.");
            Assert.IsTrue(player2.Board.Recovery[0].OwnerName == player2.Name, "Le propriétaire devrait être le joueur 2.");
        }

        [TestMethod()]
        public void EmptyStackTest_BattleWinWithNeutrals()
        {
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");
            player1.Board.Deck.Add(new Huntress(player1.Name));
            BoardBuilder.BuildArmy(player1.Board);

            Assert.IsTrue(player1.Board.Army[0].OwnerName == player1.Name, "Le propriétaire devrait être le joueur 1.");

            player1.PickCard(0);
            player1.StackCard();

            player1.EmptyStack(true, player2);

            Assert.IsTrue(player1.Board.Recovery.Count == 0, "L'infirmerie devrait contenir 0 monstre.");
            Assert.IsTrue(player2.Board.Recovery.Count == 0, "L'infirmerie devrait contenir 0 monstre.");
            Assert.IsTrue(player1.Board.Army[0].OwnerName == player1.Name, "Le propriétaire devrait être le joueur 1.");
        }
    }
}