using System;
using System.Collections.Generic;
using System.Text;
using ConsolQuest.Equipments;
using Equipments;

namespace Player
{
    class Inventory
    {
        public int torches { get; private set; }
        public int gold { get; private set; }
        private List<Item> items = new List<Item>();
        private Weapon equippedWeapon;
        private Shield equippedShield;
    }
}
