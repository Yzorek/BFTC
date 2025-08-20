namespace BattleForTheCastle.Cards.NeutralCards.Spirit.Tests
{
    [TestClass()]
    public class CrystalWraithTests
    {
        [TestMethod()]
        public void CrystalWraithTest()
        {
            var newCard = new CrystalWraith();
            Assert.IsTrue(newCard.IsImmuneToMagic);
        }
    }
}