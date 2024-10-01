using System;

namespace BattleForTheCastle.Cards
{
	public enum ElementType
	{
		Fire,
		Water,
		Air,
		Grass
	}
	public class Family
	{
		public string Name { get; }
		public string Description { get; }
		public Element Element { get; }

		public Family(string name, string description, Element element)
		{
			Name = name;
			Description = description;
			Element = element;
		}

		public void Ability()
		{
			Console.WriteLine("ability on!");
		}
	}

	public class Element
	{
		public string Description { get; set; }
		public ElementType Type { get; set; }

		public Element(string description, ElementType element)
		{
			Description = description;
			Type = element;
		}

		public void Ability()
		{
			Console.WriteLine("ability on!");
		}
	}
}