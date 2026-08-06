using System;
using System.Collections.Generic;
using System.Text;

namespace Playerr
{
    class Rogue : Player
    {
        public Rogue(int life, int actualLife, int xp, int level, string name, int damage, int defense, Inventory inventory) : base(life, actualLife, xp, level, name, damage, defense, inventory)
        {
        }

        public override void LevelUp()
        {
            IncreaseBaseAttributes(lifeGain: 2, damageGain: 4, defenseGain: 2);
        }
    }
}
