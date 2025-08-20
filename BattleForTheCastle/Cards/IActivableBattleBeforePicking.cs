namespace BattleForTheCastle.Cards
{
	public interface IActivableBattleBeforePicking
    {
		public string Text { get; }

		void Activate();
	}
}
