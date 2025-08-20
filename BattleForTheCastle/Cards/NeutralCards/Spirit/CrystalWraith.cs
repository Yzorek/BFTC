namespace BattleForTheCastle.Cards.NeutralCards.Spirit
{
    public class CrystalWraith : NeutralCard
    {
        public CrystalWraith()
        {
        }

        public CrystalWraith(string ownerName) : base(ownerName)
        {
        }

        public override void Initialize()
        {
            Name = "Spectre de cristal";
            Text = "Ce monstre est immunisé aux effets des cartes magies (négatifs ou positifs).";
            BaseAttack = 19;
            EffectiveAttack = 19;
            Rank = 4;
            Food = 2;
            Category = Category.Spirit;
            IsImmuneToMagic = true;
        }
    }
}
