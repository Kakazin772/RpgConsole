using Equipments;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolQuest.Equipments
{
    class Weapon : Item
    {
        private int AttackBonus;

        public Weapon(string name, int attackBonus) : base(name)
        {
            AttackBonus = attackBonus;
        }

        public override int UseItem()
        {
            return AttackBonus;
        }
    }
}
