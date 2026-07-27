using System;
using System.Collections.Generic;
using System.Text;

namespace Player
{
    class Warrior : Player
    {
        public Warrior(int life, int actualLife, int xp, int level, string name, int damage, int defense, Inventory inventory) : base(life, actualLife, xp, level, name, damage, defense, inventory)
        {
        }

        public override void LevelUp()
        {
            IncreaseBaseAttributes(lifeGain: 5, damageGain: 3, defenseGain: 3);
        }

        public void SuperBlock()
        {
            // TODO: implementar quando CombatManager estiver pronto.
            // Provável comportamento: dobrar defesa temporariamente por 1 turno (mais forte que o Defend comum)
        }
    }
}
