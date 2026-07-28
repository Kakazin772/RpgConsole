using System;
using System.Collections.Generic;
using System.Text;

namespace Enemys
{
    class Boss : Enemy
    {
        public int BonusAttack { get; private set; }

        public Boss()
        {
        }

        public Boss(string name, int life, int attack, int defense, int xpReward, int level, int bonusAttack) : base(name, life, attack, defense, xpReward, level)
        {
            BonusAttack = bonusAttack;
        }

        public int SpecialAttack()
        {
            return Attack + BonusAttack;
        }
    }
}