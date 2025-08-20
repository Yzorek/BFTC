using BattleForTheCastle.Game;

namespace BattleForTheCastle.Cards
{
	public interface IActivableBattleBeforeReveal
    {
		void ActivateBeforeReveal(List<Player> players, Battle battle, Player opponent);
	}
}
