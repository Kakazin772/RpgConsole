using System;
using System.Collections.Generic;
using System.Text;

namespace Equipments
{
    class Shield : Item
    {
        private int DefenseBonus;

        public Shield(string name, int defenseBonus) : base(name)
        {
            DefenseBonus = defenseBonus;
        }
    }
}
