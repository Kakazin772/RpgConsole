using System;
using System.Collections.Generic;
using System.Text;

namespace Player
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

        public void SuperFlee()
        {
            //TODO: Implementar quando existir a classe combatManager
            //ter 100% de eficacia ao fugir mas gastar 2 tochas
        }
    }
}
