using BattleForTheCastle.Board;
using BattleForTheCastle.Game;

namespace BattleForTheCastle.Cards.NeutralCards.Undead.Tests
{
    [TestClass()]
    public class SkeletonSoldierTests
    {
        static List<Family> InitFamilies(List<Element> elements)
        {
            List<Family> newList =
            [
                new Family(FamilyType.Angel, "Ange", "Blablabla", elements[0]),
                new Family(FamilyType.Fairie, "Fee", "Blablabla", elements[1]),
            ];
            return newList;
        }

        static List<Element> InitElements()
        {
            List<Element> newList =
            [
                new Element("L'élément de l'air.", ElementType.Air),
                new Element("L'élément de la terre.", ElementType.Grass)
            ];
            return newList;
        }

        static void InitBoardAngels(PlayerBoard board, Family family)
        {
            board.Deck.Add(new FamilyCard("L'invisible", 12, 3, 1, family));
        }

        static void InitBoardFairies(PlayerBoard board, Family family)
        {
            board.Deck.Add(new FamilyCard("Fée 3", 12, 3, 1, family));
        }

        static void BuildArmy(PlayerBoard board)
        {
            int total = board.Deck.Count;
            for (int i = 0; i < total; i++)
            {
                var card = board.Deck.First();
                board.Deck.Remove(card);
                board.Army.Add(card);
            }
        }

        [TestMethod()]
        public void ActivateTest_VersusOtherThanFairie()
        {
            List<Element> elements = InitElements();
            List<Family> families = InitFamilies(elements);
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");
            player1.Board.Deck.Add(new SkeletonSoldier("Soldat squelette", 9, 2, 1, Category.Undead, "Possède +4 ATK contre les fées."));
            InitBoardAngels(player2.Board, families[0]);
            BuildArmy(player1.Board);
            BuildArmy(player2.Board);

            player1.PickCard(0);
            player1.StackCard();
            player2.PickCard(0);
            player2.StackCard();

            ((IActivableBattleBeforeReveal)player1.PickedCard).Activate([player1, player2], new Battle(player1, player2, ElementType.Fire), player2);

            Assert.IsTrue(((MonsterCard)player1.PickedCard).EffectiveAttack == 9, "L'attaque effective devrait être de 9.");
        }

        [TestMethod()]
        public void ActivateTest_VersusFairie()
        {
            List<Element> elements = InitElements();
            List<Family> families = InitFamilies(elements);
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");
            player1.Board.Deck.Add(new SkeletonSoldier("Soldat squelette", 9, 2, 1, Category.Undead, "Possède +4 ATK contre les fées."));
            InitBoardFairies(player2.Board, families[1]);
            BuildArmy(player1.Board);
            BuildArmy(player2.Board);

            player1.PickCard(0);
            player1.StackCard();
            player2.PickCard(0);
            player2.StackCard();

            ((IActivableBattleBeforeReveal)player1.PickedCard).Activate([player1, player2], new Battle(player1, player2, ElementType.Fire), player2);

            Assert.IsTrue(((MonsterCard) player1.PickedCard).EffectiveAttack == 13, "L'attaque effective devrait être de 13.");
        }
    }
}