using System;
using Equipments;
using Playerr;
using Manager;

namespace ConsolQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            Inventory inventory = new Inventory(10, 20);
            
            Item EscudoMilFaces = new Shield("Escudo das mil faces", 20);

            Player player = new Warrior(20, 20, 20, 10, "Joao", 10, 5, inventory);

            player.Inventory.AddItem(EscudoMilFaces);

            GameManager game = new GameManager();

            game.StartGame();

            player.Inventory.ViewInventory();
        }
    }
}