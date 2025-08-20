using BattleForTheCastle.Game;

namespace BattleForTheCastle.Cards.NeutralCards.Human
{
    public class Assassin : NeutralCard, IActivableBattleBeforeReveal
    {
        public Assassin()
        {
        }

        public Assassin(string ownerName) : base(ownerName)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            Name = "Assassin";
            Text = "Ce monstre a -10 ATK contre les bubbles.";
            BaseAttack = 16;
            EffectiveAttack = 16;
            Rank = 3;
            Food = 1;
            Category = Category.Human;
        }

        public void ActivateBeforeReveal(List<Player> players, Battle battle, Player opponent)
        {
            if (IsDisabled)
                return;
            if (opponent.PickedCard is FamilyCard familyCard)
            {
                if (familyCard.Family.Type == FamilyType.Bubble)
                {
                    EffectiveAttack -= 10;
                }
            }
        }
    }
}
