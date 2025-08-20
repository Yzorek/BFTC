using BattleForTheCastle.Game;

namespace BattleForTheCastle.Cards.NeutralCards.Forest
{
    public class EarthGolem : NeutralCard, IActivableBattleBeforeReveal
    {
        public EarthGolem()
        {
        }

        public EarthGolem(string ownerName) : base(ownerName)
        {
        }

        public override void Initialize()
        {
            Name = "Golem de terre";
            Text = "Un monstre d'élément air attaquant le golem de terre, voit son ATK baisser de 5.";
            BaseAttack = 22;
            EffectiveAttack = 22;
            Rank = 5;
            Food = 2;
            Category = Category.Forest;
        }

        public void ActivateBeforeReveal(List<Player> players, Battle battle, Player opponent)
        {
            if (IsDisabled)
                return;
            if (opponent.PickedCard is FamilyCard familyCard)
            {
                if (familyCard.Family.Element.Type == ElementType.Air)
                {
                    familyCard.EffectiveAttack -= 5;
                }
            }
        }
    }
}
