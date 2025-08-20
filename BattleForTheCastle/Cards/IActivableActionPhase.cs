namespace BattleForTheCastle.Cards
{
	public interface IActivableActionPhase
	{
		public string Text { get; }

		void Activate();
	}
}
