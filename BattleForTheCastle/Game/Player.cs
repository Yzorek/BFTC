using BattleForTheCastle.Board;
using BattleForTheCastle.Cards;

namespace BattleForTheCastle.Game
{
    public class Player
    {
        public string Name { get; set; }

        public Card? LockedCard { get; set; }

        public Card PickedCard { get; set; }

        public Stack<Card> BattleStack { get; set; }

        public PlayerBoard Board { get; set; }

        public Player(string name)
        {
            Name = name;
            BattleStack = new Stack<Card>();
            Board = new PlayerBoard();
        }

        public void PickCard(int cardNumber)
        {
            //Console.WriteLine(Name + " picking...");
            var playableCards = Board.Army.Where(card => card != LockedCard).ToList();
            PickedCard = playableCards[cardNumber];
        }

        public void LockCard()
        {
            LockedCard = PickedCard;
        }

        public void UnlockCard()
        {
            LockedCard = null;
        }

        public void StackCard()
        {
            BattleStack.Push(PickedCard);
            Board.Army.Remove(PickedCard);
        }

        public bool CanPlay()
        {
            bool canPlayerPlay = true;

            if (Board.Army.Count <= 1)
            {
                if ((LockedCard != null && Board.Army.Contains(LockedCard)) || Board.Army.Count == 0)
                {
                    canPlayerPlay = false;
                }
            }

            return canPlayerPlay;
        }

        public void EmptyStack(bool isBattleWon, Player opponent)
        {
            bool isFirst = isBattleWon;
            while (BattleStack.Count > 0)
            {
                var card = BattleStack.Pop();
                if (isFirst)
                {
                    Board.Army.Add(card);
                    isFirst = false;
                    continue;
                }
                switch (card)
                {
                    case MonsterCard monsterCard:
                        monsterCard.ResetEffectiveAttack();
                        if (isBattleWon == false && monsterCard is NeutralCard)
                        {
                            monsterCard.OwnerName = opponent.Name;
                            opponent.Board.Recovery.Add(monsterCard);
                        }
                        else
                            Board.Recovery.Add(monsterCard);
                        break;
                    case MagicCard magicCard:
                        Board.UsedMagicZone.Add(magicCard);
                        break;
                }
            }
        }
    }
}
