using System;
using System.Collections.Generic;
using System.Text;

namespace Equipments
{
    class Potion : Item
    {
        public int HealBonus { get; private set; }

        public Potion(string name, int level, int healBonus) : base(name, level)
        {
            HealBonus = healBonus;
        }
    }
}
