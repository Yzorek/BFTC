using BattleForTheCastle.Game;

namespace BattleForTheCastle.Cards
{
	public interface IActivableBattleAfterLocking
    {
		void ActivateAfterLocking(List<Player> players, Battle battle, Player opponent);
	}
}
