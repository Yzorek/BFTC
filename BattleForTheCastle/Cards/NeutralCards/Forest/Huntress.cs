using BattleForTheCastle.Game;

namespace BattleForTheCastle.Cards.NeutralCards.Forest
{
    public class Huntress : NeutralCard, IActivableBattleBeforeReveal, IActivableBattleAfterLocking
    {
        public Huntress()
        {
        }

        public Huntress(string ownerName) : base(ownerName)
        {
        }

        public override void Initialize()
        {
            Name = "Chasseresse";
            Text = "Si la chasseresse est face à une carte magie, elle l'annule et peut être jouée de nouveau au prochain duel.";
            BaseAttack = 24;
            EffectiveAttack = 24;
            Rank = 5;
            Food = 2;
            Category = Category.Forest;
        }

        public void ActivateAfterLocking(List<Player> players, Battle battle, Player opponent)
        {
            if (IsDisabled)
                return;
            if (opponent.PickedCard is MagicCard && OwnerName != null)
                players.First(x => string.Equals(x.Name, OwnerName)).LockedCard = null;
        }

        public void ActivateBeforeReveal(List<Player> players, Battle battle, Player opponent)
        {
            if (IsDisabled)
                return;
            if (opponent.PickedCard is MagicCard magicCard)
                magicCard.IsDisabled = true;
        }
    }
}
