namespace BattleForTheCastle.Cards
{
	public interface IActivable
	{
		public string Text { get; }

		void Activate();
	}
}
