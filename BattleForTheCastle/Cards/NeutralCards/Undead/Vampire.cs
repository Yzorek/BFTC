using BattleForTheCastle.Game;

namespace BattleForTheCastle.Cards.NeutralCards.Undead
{
    public class Vampire : NeutralCard, IActivableBattleBeforeReveal
    {
        public Vampire()
        {
        }

        public Vampire(string ownerName) : base(ownerName)
        {
        }

        public override void Initialize()
        {
            base.Initialize();
            Name = "Vampire";
            Text = "Le vampire aspire l'énergie de son adversaire si c'est un humain, il lui vole 5 ATK.";
            BaseAttack = 18;
            EffectiveAttack = 18;
            Rank = 4;
            Food = 2;
            Category = Category.Undead;
        }

        public void ActivateBeforeReveal(List<Player> players, Battle battle, Player opponent)
        {
            if (IsDisabled)
                return;
            if (opponent.PickedCard is NeutralCard neutralCard)
            {
                if (neutralCard.Category == Category.Human)
                {
                    EffectiveAttack += 5;
                    neutralCard.EffectiveAttack -= 5;
                }
            }
        }
    }
}
