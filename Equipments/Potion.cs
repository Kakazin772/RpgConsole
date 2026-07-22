using System;
using System.Collections.Generic;
using System.Text;

namespace Equipments
{
    class Potion : Item
    {
        public int HealBonus { get; private set; }

        public Potion(string name, int healBonus) : base(name)
        {
            HealBonus = healBonus;
        }
    }
}
