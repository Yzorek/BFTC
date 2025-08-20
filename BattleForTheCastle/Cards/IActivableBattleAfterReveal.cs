namespace BattleForTheCastle.Cards
{
	public interface IActivableBattleAfterReveal
    {
		public string Text { get; }

		void Activate();
	}
}
