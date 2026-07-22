using Equipments;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolQuest.Equipments
{
    class Weapon : Item
    {
        public int AttackBonus { get; private set; }

        public Weapon(string name, int attackBonus) : base(name)
        {
            AttackBonus = attackBonus;
        }
    }
}
