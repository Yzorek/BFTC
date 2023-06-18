using System;
using System.Collections.Generic;

namespace BattleForTheCastle.Card
{
	public enum Category
	{
		Human,
		Undead,
		Forest,
		Fabulous,
		Spirit,
	}

	public abstract class Card
	{
		public string Name { get; set; }

		protected Card(string name)
		{
			Name = name;
		}
	}

	public abstract class MonsterCard : Card
	{
		public Dictionary<string, int> Stats { get; set; }

		protected MonsterCard(string name, Dictionary<string, int> stats) : base(name)
		{
			Stats = stats;
		}
	}

	public class MagicCard : Card, IActivable
	{
		public string Text { get; }

		public void Activate()
		{
			Console.WriteLine("activation!");
		}

		public MagicCard(string name, string text) : base(name)
		{
			Text = text;
		}
	}

	public class FamilyCard : MonsterCard
	{
		public Family Family { get; }

		public FamilyCard(string name, Dictionary<string, int> stats, Family family) : base(name, stats)
		{
			Family = family;
		}
	}

	public class NeutralCard : MonsterCard, IActivable
	{
		public string Text { get; set; }
		public int CopyNb { get; set; }
		public Category Category { get; set; }

		public void Activate()
		{
			Console.WriteLine("activation!");
		}

		public NeutralCard(string name, Dictionary<string, int> stats, Category category, int copyNb) : base(name, stats)
		{
			Category = category;
			CopyNb = copyNb;
		}
	}
}