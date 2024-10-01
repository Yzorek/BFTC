using BattleForTheCastle.Board;
using BattleForTheCastle.Cards;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

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

        public void PickCard()
        {
            Console.WriteLine(Name + " picking...");
            var rand = new Random();
            var playableCards1 = Board.Army.Where(card => card != LockedCard).ToList();
            PickedCard = playableCards1[rand.Next(0, playableCards1.Count)];
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

        public void EmptyStack()
        {
            bool isFirst = true;
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
                        Board.Recovery.Add(monsterCard);
                        break;
                    case MagicCard magicCard:
                        Board.UsedMagicZone.Add(magicCard);
                        break;
                }
            }
        }

        public void EmptyStackGivingNeutrals(Player playerReceivingNeutrals)
        {
            while (BattleStack.Count > 0)
            {
                var card = BattleStack.Pop();
                switch (card)
                {
                    case NeutralCard neutralCard:
                        playerReceivingNeutrals.Board.Recovery.Add(neutralCard);
                        break;
                    case FamilyCard familyCard:
                        Board.Recovery.Add(familyCard);
                        break;
                    case MagicCard magicCard:
                        Board.UsedMagicZone.Add(magicCard);
                        break;
                }
            }
        }
    }
}
