using BattleForTheCastle.Board;
using BattleForTheCastle.Cards;
using BattleForTheCastle.Game;

namespace BattleForTheCastleTests
{
    public static class BoardBuilder
    {
        static Dictionary<string, (int attack, int rank, int food, Family family)> familyCards = new Dictionary<string, (int attack, int rank, int food, Family family)>()
        {
            { "Ange 1", (5, 1, 1, FamilyBuilder.Families["Ange"]) },
            { "Ange 2", (10, 2, 1, FamilyBuilder.Families["Ange"]) },
            { "L'invisible",(12, 3, 1, FamilyBuilder.Families["Ange"]) },
            { "Dame céleste", (20, 4, 2, FamilyBuilder.Families["Ange"]) },
            { "Cupidon", (22, 5, 2, FamilyBuilder.Families["Ange"]) },
            { "Archange", (35, 6, 3, FamilyBuilder.Families["Ange"]) },
            { "Séraphin", (45, 7, 3, FamilyBuilder.Families["Ange"]) },
            { "Bébé dragon", (5, 1, 1, FamilyBuilder.Families["Dragon"]) },
            { "Jeune dragon", (10, 2, 1, FamilyBuilder.Families["Dragon"]) },
            { "Dragon azur", (15, 3, 2, FamilyBuilder.Families["Dragon"]) },
            { "Dragon or", (20, 4, 2, FamilyBuilder.Families["Dragon"]) },
            { "Ancien dragon", (25, 5, 3, FamilyBuilder.Families["Dragon"]) },
            { "Ecaille rouge", (30, 6, 3, FamilyBuilder.Families["Dragon"]) },
            { "Le déstructeur", (50, 7, 4, FamilyBuilder.Families["Dragon"]) },
            { "Fée 1", (2, 1, 1, FamilyBuilder.Families["Fee"]) },
            { "Fée 2", (7, 2, 1, FamilyBuilder.Families["Fee"]) },
            { "Fée 3", (12, 3, 1, FamilyBuilder.Families["Fee"]) },
            { "Fée 4", (19, 4, 2, FamilyBuilder.Families["Fee"]) },
            { "Fée 5", (24, 5, 2, FamilyBuilder.Families["Fee"]) },
            { "Fée 6", (35, 6, 3, FamilyBuilder.Families["Fee"]) },
            { "Fée 7", (50, 7, 4, FamilyBuilder.Families["Fee"]) },
            { "Bébé bubble", (3, 1, 1, FamilyBuilder.Families["Bubble"]) },
            { "Micro bubble", (5, 2, 1, FamilyBuilder.Families["Bubble"]) },
            { "Mini bubble", (10, 3, 1, FamilyBuilder.Families["Bubble"]) },
            { "Moyen bubble", (17, 4, 1, FamilyBuilder.Families["Bubble"]) },
            { "Grand bubble", (22, 5, 1, FamilyBuilder.Families["Bubble"]) },
            { "Double bubble", (28, 6, 2, FamilyBuilder.Families["Bubble"]) },
            { "Maxi bubble", (42, 7, 2, FamilyBuilder.Families["Bubble"]) },
        };

        public static void BuildArmy(PlayerBoard board)
        {
            int total = board.Deck.Count;
            for (int i = 0; i < total; i++)
            {
                var card = board.Deck.First();
                board.Deck.Remove(card);
                board.Army.Add(card);
            }
        }

        public static void InitBoard(List<string> names, Player player)
        {
            foreach (string name in names)
            {
                player.Board.Deck.Add(new FamilyCard(
                    name,
                    familyCards[name].attack,
                    familyCards[name].rank,
                    familyCards[name].food,
                    familyCards[name].family,
                    player.Name));
            }
        }
    }
}
