using System;
using System.Collections.Generic;
using System.Text;

namespace Equipments
{
    class Potion : Item
    {
        private int HealBonus;

        public Potion(string name, int healBonus) : base(name)
        {
            HealBonus = healBonus;
        }

        public override int UseItem()
        {
            return HealBonus;
        }
    }
}
