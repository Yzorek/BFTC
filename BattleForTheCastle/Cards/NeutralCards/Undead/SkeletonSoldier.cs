using BattleForTheCastle.Game;

namespace BattleForTheCastle.Cards.NeutralCards.Undead
{
    public class SkeletonSoldier : NeutralCard, IActivableBattleBeforeReveal
    {
        public SkeletonSoldier(string name, int attack, int rank, int food, Category category, string text) : base(name, attack, rank, food, category, text)
        {
        }

        public void Activate(List<Player> players, Battle battle, Player opponent)
        {
            if (opponent.PickedCard is FamilyCard familyCard)
            {
                if (familyCard.Family.Type == FamilyType.Fairie)
                {
                   EffectiveAttack += 4;
                }
            }
        }
    }
}
