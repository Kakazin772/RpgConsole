using System;
using System.Collections.Generic;
using System.Text;

namespace Equipments
{
    class Shield : Item
    {
        public int DefenseBonus { get; private set; }

        public Shield(string name, int level, int defenseBonus) : base(name, level)
        {
            DefenseBonus = defenseBonus;
        }
    }
}
