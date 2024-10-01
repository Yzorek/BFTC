using System;
using System.Collections.Generic;
using System.Linq;
using BattleForTheCastle.Board;
using BattleForTheCastle.Cards;
using BattleForTheCastle.Game;

namespace BattleForTheCastle
{
	class Program
	{
		static List<Family> InitFamilies(List<Element> elements)
		{
			List<Family> newList =
            [
                new Family("Ange", "Blablabla", elements[0]),
                new Family("Dragon", "Blablabla", elements[1]),
                new Family("Fee", "Blablabla", elements[2]),
                new Family("Bubble", "Blablabla", elements[3]),
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

		static void InitBoard1(PlayerBoard board, Family family)
		{
			board.Deck.Add(new FamilyCard("Ange 1", 5, 1, 1, family));
			board.Deck.Add(new FamilyCard("Ange 2", 10, 2, 1, family));
			board.Deck.Add(new FamilyCard("L'invisible", 12, 3, 1, family));
			board.Deck.Add(new FamilyCard("Dame céleste", 20, 4, 2, family));
			board.Deck.Add(new FamilyCard("Cupidon", 22, 5, 2, family));
			board.Deck.Add(new FamilyCard("Archange", 35, 6, 3, family));
			board.Deck.Add(new FamilyCard("Séraphin", 45, 7, 3, family));
		}

		static void InitBoard2(PlayerBoard board, Family family)
		{
			board.Deck.Add(new FamilyCard("Bébé dragon", 5, 1, 1, family));
			board.Deck.Add(new FamilyCard("Jeune dragon", 10, 2, 1, family));
			board.Deck.Add(new FamilyCard("Dragon azur", 15, 3, 2, family));
			board.Deck.Add(new FamilyCard("Dragon or", 20, 4, 2, family));
			board.Deck.Add(new FamilyCard("Ancien dragon", 25, 5, 3, family));
			board.Deck.Add(new FamilyCard("Ecaille rouge", 30, 6, 3, family));
			board.Deck.Add(new FamilyCard("Le déstructeur", 50, 7, 4, family));
		}

		static void BuildArmies(PlayerBoard board1, PlayerBoard board2)
		{
			for (int i = 0; i < 3; i++)
			{
				var card = board1.Deck.First();
				board1.Deck.Remove(card);
				board1.Army.Add(card);
				card = board2.Deck.First();
				board2.Deck.Remove(card);
				board2.Army.Add(card);
			}
		}

		static void Main(string[] args)
		{
			Player player1 = new Player("Player 1");
			Player player2 = new Player("Player 2");

			List<Element> elements = InitElements();
			List<Family> families = InitFamilies(elements);

			InitBoard1(player1.Board, families[0]);
			InitBoard2(player2.Board, families[1]);
			BuildArmies(player1.Board, player2.Board);

			Battle newBattle = new Battle(player1, player2, ElementType.Air);
			newBattle.Start();
			Console.WriteLine(newBattle.Results.ToString());
		}
	}
}
