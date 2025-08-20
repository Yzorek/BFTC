using BattleForTheCastle.Cards.NeutralCards.Undead;
using BattleForTheCastle.Cards;
using BattleForTheCastle.Game;
using BattleForTheCastle.Cards.NeutralCards.Human;
using BattleForTheCastle.Cards.NeutralCards.Forest;

namespace BattleForTheCastleTests.Cards.NeutralCards.Undead
{
    [TestClass()]
    public class VampireTests
    {
        [TestMethod()]
        public void ActivateBeforeRevealTest_VersusOtherThanHuman()
        {
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");

            player1.Board.Deck.Add(new Vampire(player1.Name));
            player2.Board.Deck.Add(new Huntress(player2.Name));

            BoardBuilder.BuildArmy(player1.Board);
            BoardBuilder.BuildArmy(player2.Board);

            player1.PickCard(0);
            player1.StackCard();
            player2.PickCard(0);
            player2.StackCard();

            ((IActivableBattleBeforeReveal)player1.PickedCard).ActivateBeforeReveal([player1, player2], new Battle(player1, player2, ElementType.Fire), player2);

            Assert.IsTrue(((MonsterCard)player1.PickedCard).EffectiveAttack == 18, "L'attaque effective devrait être de 18.");
            Assert.IsTrue(((MonsterCard)player2.PickedCard).EffectiveAttack == 24, "L'attaque effective devrait être de 24.");
        }

        [TestMethod()]
        public void ActivateBeforeRevealTest_VersusHuman()
        {
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");

            player1.Board.Deck.Add(new Vampire(player1.Name));
            player2.Board.Deck.Add(new Assassin(player2.Name));

            BoardBuilder.BuildArmy(player1.Board);
            BoardBuilder.BuildArmy(player2.Board);

            player1.PickCard(0);
            player1.StackCard();
            player2.PickCard(0);
            player2.StackCard();

            ((IActivableBattleBeforeReveal)player1.PickedCard).ActivateBeforeReveal([player1, player2], new Battle(player1, player2, ElementType.Fire), player2);

            Assert.IsTrue(((MonsterCard)player1.PickedCard).EffectiveAttack == 23, "L'attaque effective devrait être de 23.");
            Assert.IsTrue(((MonsterCard)player2.PickedCard).EffectiveAttack == 11, "L'attaque effective devrait être de 11.");
        }
    }
}
