namespace BattleForTheCastle.Card
{
	public interface IActivable
	{
		public string Text { get; }

		void Activate();
	}
}
