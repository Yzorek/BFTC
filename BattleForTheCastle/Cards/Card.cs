using BattleForTheCastle.Game;

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
		public string? Name { get; set; }

		public string? OwnerName { get; set; } = null;

		public bool IsDisabled { get; set; } = false;
	}

	public abstract class MonsterCard : Card
	{
		public int BaseAttack { get; set; }

		public int EffectiveAttack { get; set; }

        public int Rank { get; set; }

        public int Food { get; set; }

		public void ResetEffectiveAttack()
		{
			EffectiveAttack = BaseAttack;
		}
	}

	public class MagicCard : Card, IActivableBattleBeforeReveal
	{
		public string Text { get; }

		public void ActivateBeforeReveal(List<Player> players, Battle battle, Player opponent)
		{
			Console.WriteLine("activation!");
		}

		public MagicCard(string name, string text)
		{
			Text = text;
		}
	}

	public class FamilyCard : MonsterCard
	{
		public Family Family { get; }

		public FamilyCard(string name, int attack, int rank, int food, Family family, string ownerName)
		{
			Name = name;
            BaseAttack = attack;
            EffectiveAttack = attack;
            Rank = rank;
            Food = food;
            Family = family;
			OwnerName = ownerName;
        }
	}

	public abstract class NeutralCard : MonsterCard
	{
		public string? Text { get; set; }
        public Category Category { get; set; }

		public NeutralCard()
		{
			Initialize();
        }

        public NeutralCard(string ownerName)
        {
			Initialize();
            OwnerName = ownerName;
        }

		public virtual void Initialize()
		{
		}
    }
}