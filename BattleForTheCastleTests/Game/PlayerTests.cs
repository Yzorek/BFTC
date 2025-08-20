using BattleForTheCastle.Board;
using BattleForTheCastle.Cards;

namespace BattleForTheCastle.Game.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        static List<Family> InitFamilies(List<Element> elements)
        {
            List<Family> newList =
            [
                new Family(FamilyType.Angel, "Ange", "Blablabla", elements[0]),
                new Family(FamilyType.Dragon, "Dragon", "Blablabla", elements[1]),
            ];
            return newList;
        }

        static List<Element> InitElements()
        {
            List<Element> newList =
            [
                new Element("L'élément de l'air.", ElementType.Air),
                new Element("L'élément du feu.", ElementType.Fire),
            ];
            return newList;
        }

        static void InitBoardAngels(PlayerBoard board, Family family)
        {
            board.Deck.Add(new FamilyCard("Ange 1", 5, 1, 1, family));
            board.Deck.Add(new FamilyCard("Ange 2", 10, 2, 1, family));
            board.Deck.Add(new FamilyCard("L'invisible", 12, 3, 1, family));
        }

        static void InitBoardDragons(PlayerBoard board, Family family)
        {
            board.Deck.Add(new FamilyCard("Bébé dragon", 5, 1, 1, family));
            board.Deck.Add(new FamilyCard("Jeune dragon", 10, 2, 1, family));
            board.Deck.Add(new FamilyCard("Dragon azur", 15, 3, 2, family));
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
        public void EmptyStackTest_BattleLoss()
        {
            List<Element> elements = InitElements();
            List<Family> families = InitFamilies(elements);
            Player player1 = new Player("Player 1");

            InitBoardAngels(player1.Board, families[0]);
            BuildArmy(player1.Board);

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
            List<Element> elements = InitElements();
            List<Family> families = InitFamilies(elements);
            Player player2 = new Player("Player 2");
            InitBoardDragons(player2.Board, families[1]);
            BuildArmy(player2.Board);

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
            player1.Board.Deck.Add(new NeutralCard("Chasseresse", 24, 5, 2, Category.Forest, "Si la chasseresse est face à une carte magie, elle l'annule et peut être jouée de nouveau au prochain duel."));
            BuildArmy(player1.Board);

            player1.PickCard(0);
            player1.StackCard();

            player1.EmptyStack(false, player2);

            Assert.IsTrue(player1.Board.Recovery.Count == 0, "L'infirmerie devrait contenir 0 monstre.");
            Assert.IsTrue(player2.Board.Recovery.Count == 1, "L'infirmerie devrait contenir 1 monstre.");
        }

        [TestMethod()]
        public void EmptyStackTest_BattleWinWithNeutrals()
        {
            Player player1 = new Player("Player 1");
            Player player2 = new Player("Player 2");
            player1.Board.Deck.Add(new NeutralCard("Chasseresse", 24, 5, 2, Category.Forest, "Si la chasseresse est face à une carte magie, elle l'annule et peut être jouée de nouveau au prochain duel."));
            BuildArmy(player1.Board);

            player1.PickCard(0);
            player1.StackCard();

            player1.EmptyStack(true, player2);

            Assert.IsTrue(player1.Board.Recovery.Count == 0, "L'infirmerie devrait contenir 0 monstre.");
            Assert.IsTrue(player2.Board.Recovery.Count == 0, "L'infirmerie devrait contenir 0 monstre.");
        }
    }
}