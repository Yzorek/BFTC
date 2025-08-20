using BattleForTheCastle.Game;

namespace BattleForTheCastle.Cards
{
	public interface IActivableBattleBeforeReveal
	{
		public string Text { get; }

		void Activate(List<Player> players, Battle battle, Player opponent);
	}
}
