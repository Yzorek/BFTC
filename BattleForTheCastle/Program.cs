using System;
using System.Collections.Generic;
using System.Linq;
using BattleForTheCastle.Card;

namespace BattleForTheCastle
{
	class Program
	{
		static List<Family> InitFamilies(List<Element> elements)
		{
			List<Family> newList = new List<Family>();

			newList.Add(new Family("Ange", "Blablabla", elements[0]));
			newList.Add(new Family("Dragon", "Blablabla", elements[1]));
			newList.Add(new Family("Fee", "Blablabla", elements[2]));
			newList.Add(new Family("Bubble", "Blablabla", elements[3]));
			return newList;
		}

		static List<Element> InitElements()
		{
			List<Element> newList = new List<Element>();

			newList.Add(new Element("L'élément de l'air.", ElementType.Air));
			newList.Add(new Element("L'élément du feu.", ElementType.Fire));
			newList.Add(new Element("L'élément de la nature.", ElementType.Grass));
			newList.Add(new Element("L'élément de l'eau", ElementType.Water));
			return newList;
		}

		static void InitBoard1(Board.Board board, Family family)
		{
			board.Deck.Add(new FamilyCard("Ange 1", new Dictionary<string, int>
			{
				{ "ATK", 5 },
				{ "Rank", 1 },
				{ "Food", 1 },
			}, family));
			board.Deck.Add(new FamilyCard("Ange 2", new Dictionary<string, int>
			{
				{ "ATK", 10 },
				{ "Rank", 2 },
				{ "Food", 1 },
			}, family));
			board.Deck.Add(new FamilyCard("L'invisible", new Dictionary<string, int>
			{
				{ "ATK", 12 },
				{ "Rank", 3 },
				{ "Food", 1 },
			}, family));
			board.Deck.Add(new FamilyCard("Dame céleste", new Dictionary<string, int>
			{
				{ "ATK", 20 },
				{ "Rank", 4 },
				{ "Food", 2 },
			}, family));
			board.Deck.Add(new FamilyCard("Cupidon", new Dictionary<string, int>
			{
				{ "ATK", 22 },
				{ "Rank", 5 },
				{ "Food", 2 },
			}, family));
			board.Deck.Add(new FamilyCard("Archange", new Dictionary<string, int>
			{
				{ "ATK", 35 },
				{ "Rank", 6 },
				{ "Food", 3 },
			}, family));
			board.Deck.Add(new FamilyCard("Séraphin", new Dictionary<string, int>
			{
				{ "ATK", 45 },
				{ "Rank", 7 },
				{ "Food", 3 },
			}, family));
		}

		static void InitBoard2(Board.Board board, Family family)
		{
			board.Deck.Add(new FamilyCard("Bébé dragon", new Dictionary<string, int>
			{
				{ "ATK", 5 },
				{ "Rank", 1 },
				{ "Food", 1 },
			}, family));
			board.Deck.Add(new FamilyCard("Jeune dragon", new Dictionary<string, int>
			{
				{ "ATK", 10 },
				{ "Rank", 2 },
				{ "Food", 1 },
			}, family));
			board.Deck.Add(new FamilyCard("Dragon azur", new Dictionary<string, int>
			{
				{ "ATK", 15 },
				{ "Rank", 3 },
				{ "Food", 2 },
			}, family));
			board.Deck.Add(new FamilyCard("Dragon or", new Dictionary<string, int>
			{
				{ "ATK", 20 },
				{ "Rank", 4 },
				{ "Food", 2 },
			}, family));
			board.Deck.Add(new FamilyCard("Ancien dragon", new Dictionary<string, int>
			{
				{ "ATK", 25 },
				{ "Rank", 5 },
				{ "Food", 3 },
			}, family));
			board.Deck.Add(new FamilyCard("Ecaille rouge", new Dictionary<string, int>
			{
				{ "ATK", 30 },
				{ "Rank", 6 },
				{ "Food", 3 },
			}, family));
			board.Deck.Add(new FamilyCard("Le déstructeur", new Dictionary<string, int>
			{
				{ "ATK", 50 },
				{ "Rank", 7 },
				{ "Food", 4 },
			}, family));
		}

		static void BuildArmies(Board.Board board1, Board.Board board2)
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
			Board.Board board1 = new Board.Board();
			Board.Board board2 = new Board.Board();

			List<Element> elements = InitElements();
			List<Family> families = InitFamilies(elements);

			InitBoard1(board1, families[0]);
			InitBoard2(board2, families[1]);
			BuildArmies(board1, board2);

			Battle.Battle newBattle = new Battle.Battle(board1, board2, ElementType.Air);
			newBattle.Start();
			Console.WriteLine(newBattle.Results.ToString());
		}
	}
}
