using System;
using System.Collections.Generic;
using System.Text;

namespace Enemys
{
    class Enemy
    {
        public string Name { get; private set; }
        public int Life { get; private set; }
        public int Attack { get; private set; }
        public int Defense { get; private set; }
        public int XpReward { get; private set; }
        public int Level { get; private set; }
        
        public Enemy()
        {
        }

        public Enemy(string name, int life, int attack, int defense, int xpReward, int level)
        {
            Life = life;
            Name = name;
            Attack = attack;
            Defense = defense;
            XpReward = xpReward;
            Level = level;
        }

        public void TakeDamage(int damage)
        {
            Life = Math.Max(Life - damage, 0);
        }
    }
}
