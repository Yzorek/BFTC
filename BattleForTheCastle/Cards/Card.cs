using BattleForTheCastle.Game;
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
		public int BaseAttack { get; set; }

		public int EffectiveAttack { get; set; }

        public int Rank { get; set; }

        public int Food { get; set; }

        protected MonsterCard(string name, int attack, int rank, int food) : base(name)
		{
			BaseAttack = attack;
			EffectiveAttack = attack;
            Rank = rank;
			Food = food;
		}

		public void ResetEffectiveAttack()
		{
			EffectiveAttack = BaseAttack;
		}
	}

	public class MagicCard : Card, IActivableBattleBeforeReveal
	{
		public string Text { get; }

		public void Activate(List<Player> players, Battle battle, Player opponent)
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

	public class NeutralCard : MonsterCard
	{
		public string Text { get; set; }
        public Category Category { get; set; }

        public NeutralCard(string name, int attack, int rank, int food, Category category, string text) : base(name, attack, rank, food)
        {
            Text = text;
            Category = category;
        }
    }
}