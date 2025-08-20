using BattleForTheCastle.Cards;

namespace BattleForTheCastleTests
{
    public static class FamilyBuilder
    {
        public static Dictionary<string, Element> Elements = new Dictionary<string, Element>()
        {
            { "Air", new Element("L'élément de l'air.", ElementType.Air) },
            { "Fire", new Element("L'élément du feu.", ElementType.Fire) },
            { "Grass", new Element("L'élément de la nature.", ElementType.Grass) },
            { "Water", new Element("L'élément de l'eau.", ElementType.Water) }
        };

        public static Dictionary<string, Family> Families = new Dictionary<string, Family>()
        {
            { "Ange", new Family(FamilyType.Angel, "Ange", "Blablabla", Elements["Air"]) },
            { "Dragon", new Family(FamilyType.Dragon, "Dragon", "Blablabla", Elements["Fire"]) },
            { "Fee", new Family(FamilyType.Fairie, "Fee", "Blablabla", Elements["Grass"]) },
            { "Bubble", new Family(FamilyType.Bubble, "Bubble", "Blablabla", Elements["Water"]) },
        };

        public static List<Family> InitFamilies(List<string> names)
        {
            List<Family> newList = new List<Family>();

            foreach (string name in names)
            {
                newList.Add(Families[name]);
            }

            return newList;
        }
    }
}
