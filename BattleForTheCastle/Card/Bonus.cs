using System;

namespace BattleForTheCastle.Card
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
		public string Text { get; set; }
		public ElementType Type { get; set; }

		public Element(string text, ElementType element)
		{
			Text = text;
			Type = element;
		}

		public void Ability()
		{
			Console.WriteLine("ability on!");
		}
	}
}