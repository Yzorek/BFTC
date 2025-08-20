using BattleForTheCastle.Game;

namespace BattleForTheCastle.Cards.NeutralCards.Undead
{
    public class SkeletonSoldier : NeutralCard, IActivableBattleBeforeReveal
    {
        public SkeletonSoldier()
        {
        }

        public SkeletonSoldier(string ownerName) : base(ownerName)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            Name = "Soldat squelette";
            Text = "Possède +4 ATK contre les fées.";
            BaseAttack = 9;
            EffectiveAttack = 9;
            Rank = 2;
            Food = 1;
            Category = Category.Undead;
        }

        public void ActivateBeforeReveal(List<Player> players, Battle battle, Player opponent)
        {
            if (IsDisabled)
                return;
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
