namespace BattleForTheCastle.Cards.NeutralCards.Spirit
{
    public class JinnOfTheForgottenRegions : NeutralCard, IProtectableMagicCard
    {
        public JinnOfTheForgottenRegions()
        {
        }

        public JinnOfTheForgottenRegions(string ownerName) : base(ownerName)
        {
        }

        public override void Initialize()
        {
            Name = "Djinn des régions oubliées";
            Text = "Tant que ce monstre est dans votre armée, personne ne peut vous voler de carte magie.";
            BaseAttack = 20;
            EffectiveAttack = 20;
            Rank = 4;
            Food = 2;
            Category = Category.Spirit;
        }
    }
}
