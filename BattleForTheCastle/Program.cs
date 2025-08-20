using BattleForTheCastle.Board;
using BattleForTheCastle.Cards;
using BattleForTheCastle.Cards.NeutralCards.Forest;
using BattleForTheCastle.Game;

namespace BattleForTheCastle
{
	class Program
	{
		static List<Family> InitFamilies(List<Element> elements)
		{
			List<Family> newList =
            [
                new Family(FamilyType.Angel, "Ange", "Blablabla", elements[0]),
                new Family(FamilyType.Dragon, "Dragon", "Blablabla", elements[1]),
                new Family(FamilyType.Fairie, "Fee", "Blablabla", elements[2]),
                new Family(FamilyType.Bubble, "Bubble", "Blablabla", elements[3]),
            ];
			return newList;
		}

		static List<Element> InitElements()
		{
			List<Element> newList =
            [
                new Element("L'élément de l'air.", ElementType.Air),
                new Element("L'élément du feu.", ElementType.Fire),
                new Element("L'élément de la nature.", ElementType.Grass),
                new Element("L'élément de l'eau", ElementType.Water),
            ];
			return newList;
		}

		static void InitBoardAngels(Player player, Family family)
		{
            player.Board.Deck.Add(new FamilyCard("Ange 1", 5, 1, 1, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Ange 2", 10, 2, 1, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("L'invisible", 12, 3, 1, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Dame céleste", 20, 4, 2, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Cupidon", 22, 5, 2, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Archange", 35, 6, 3, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Séraphin", 45, 7, 3, family, player.Name));
		}

		static void InitBoardDragons(Player player, Family family)
		{
            player.Board.Deck.Add(new FamilyCard("Bébé dragon", 5, 1, 1, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Jeune dragon", 10, 2, 1, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Dragon azur", 15, 3, 2, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Dragon or", 20, 4, 2, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Ancien dragon", 25, 5, 3, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Ecaille rouge", 30, 6, 3, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Le déstructeur", 50, 7, 4, family, player.Name));
		}
        static void InitBoardFairies(Player player, Family family)
        {
            player.Board.Deck.Add(new FamilyCard("Fée 1", 2, 1, 1, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Fée 2", 7, 2, 1, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Fée 3", 12, 3, 1, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Fée 4", 19, 4, 2, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Fée 5", 24, 5, 2, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Fée 6", 35, 6, 3, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Fée 7", 50, 7, 4, family, player.Name));
        }
        static void InitBoardBubbles(Player player, Family family)
        {
            player.Board.Deck.Add(new FamilyCard("Bébé bubble", 3, 1, 1, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Micro bubble", 5, 2, 1, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Mini bubble", 10, 3, 1, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Moyen bubble", 17, 4, 1, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Grand bubble", 22, 5, 1, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Double bubble", 28, 6, 2, family, player.Name));
            player.Board.Deck.Add(new FamilyCard("Maxi bubble", 42, 7, 2, family, player.Name));
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

        static void Main(string[] args)
		{
            List<Element> elements = InitElements();
            List<Family> families = InitFamilies(elements);
            string docPath =
              Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

			for (int j = 0; j < 2; j++)
			{
                int totalBattles = 1000;

                double nbOfP1Wins = 0;
                double nbOfP2Wins = 0;
                double nbDraws = 0;

                var random = new Random();

                for (int i = 0; i < totalBattles; i++)
                {
                    Player player1 = new Player("Player 1");
                    Player player2 = new Player("Player 2");

                    //InitBoardAngels(player1.Board, families[0]);
                    InitBoardDragons(player1, families[1]);
                    InitBoardFairies(player2, families[2]);
                    if (j == 1)
                    {
                        player2.Board.Deck.Add(new Huntress());
                        //player2.Board.Deck.Add(new NeutralCard("Djinn des régions oubliées", 20, 4, 2, Category.Forest, "Tant que ce monstre est dans votre armée, personne ne peut vous voler de carte magie."));
                    }
                    //InitBoardBubbles(player2.Board, families[3]);
                    BuildArmy(player1.Board);
                    BuildArmy(player2.Board);

                    Battle newBattle = new Battle(player1, player2, ElementType.Air);
                    newBattle.Start(random);
                    if (newBattle.Results == BattleResults.Player1Win)
                    {
                        nbOfP1Wins++;
                    }
                    if (newBattle.Results == BattleResults.Player2Win)
                    {
                        nbOfP2Wins++;
                    }
                    if (newBattle.Results == BattleResults.Draw)
                    {
                        nbDraws++;
                    }
                }
                if (j == 0)
                    Console.WriteLine("Result of Battle without neutral :");
                else
                    Console.WriteLine("Result of Battle with neutral :");
                Console.WriteLine("Total P1 wins: " + (nbOfP1Wins / totalBattles * 100).ToString() + "%");
                Console.WriteLine("Total P2 wins: " + (nbOfP2Wins / totalBattles * 100).ToString() + "%");
                Console.WriteLine("Total draws: " + (nbDraws / totalBattles * 100).ToString() + "%");
                Console.WriteLine(Environment.NewLine);
            }

			//using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, "Results.txt")))
			//{
			//	outputFile.WriteLine("Total P1 wins: " + (nbOfP1Wins / totalBattles * 100).ToString() + "%");
			//	outputFile.WriteLine("Total P2 wins: " + (nbOfP2Wins / totalBattles * 100).ToString() + "%");
			//	outputFile.WriteLine("Total draws: " + (nbDraws / totalBattles * 100).ToString() + "%");
			//}
		}
	}
}
