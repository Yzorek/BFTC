using System;
using System.Collections.Generic;

namespace BattleForTheCastle.Cards
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
		public int Attack {  get; set; }

        public int Rank { get; set; }

        public int Food { get; set; }

        protected MonsterCard(string name, int attack, int rank, int food) : base(name)
		{
			Attack = attack;
			Rank = rank;
			Food = food;
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

		public FamilyCard(string name, int attack, int rank, int food, Family family) : base(name, attack, rank, food)
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

		public NeutralCard(string name, int attack, int rank, int food, Category category, int copyNb) : base(name, attack, rank, food)
		{
			Category = category;
			CopyNb = copyNb;
		}
	}
}