using ConsolQuest;
using ConsolQuest.Equipments;
using Equipments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Playerr
{
    class Inventory
    {
        public int torches { get; private set; }
        public int gold { get; private set; }
        private List<Item> items = new List<Item>();
        private Weapon equippedWeapon;
        private Shield equippedShield;

        public Inventory()
        {
        }

        public Inventory(int torches, int gold)
        {
            this.torches = torches;
            this.gold = gold;
        }

        public void AddGold(int amount)
        {
            gold = gold + amount;
        }

        public void ConsumeTorch()
        {
            torches = torches - 1;
        }

        public void AddItem(Item item)
        {
            items.Add(item);
        }

        public void RemoveItem(Item item)
        {
            items.Remove(item);
        }

        public void ViewInventory()
        {
            Console.WriteLine("=============================");
            Console.WriteLine($"Ouro: {gold} | Tochas: {torches}");
            Console.WriteLine("-- Itens --");

            foreach (Item item in items)
            {
                string status = (item == equippedWeapon || item == equippedShield) ? " [Equipado]" : "";
                Console.WriteLine($"{item.Name}{status}");
            }

            Console.WriteLine("=============================");
        }

        public void EquipItem(Item item)
        {
            if (!items.Contains(item))
            {
                throw new GameException("Item não está no inventário.");
            }

            if (item is Weapon weapon)
            {
                equippedWeapon = weapon;
            }
            else
            {
                if (item is Shield shield)
                {
                    equippedShield = shield;
                }
                else
                {
                    throw new GameException("Este item não pode ser equipado.");
                }
            }
        }

        public int UsePotion(Potion potion)
        {
            if (!items.Contains(potion))
            {
                throw new GameException("Poção não está no inventário.");
            }

            items.Remove(potion);
            return potion.HealBonus;
        }
    }
}
