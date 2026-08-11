using ConsolQuest;
using ConsolQuest.Equipments;
using Dungeon;
using Enemys;
using Equipments;
using Playerr;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Manager
{
    class GameManager
    {
        public CombatManager Combat { get; private set; }
        public Player player { get; private set; }
        public List<Room> Dungeon { get; private set; } = new List<Room>();

        public int CurrentRoomIndex { get; private set; }
        public int MonsterDefeated { get; private set; }

        public void StartGame()
        {
            string name;
            int classe;

            Inventory inventory = new Inventory(10, 0);
            
            Console.WriteLine("Bem vindo ao mundo de ConsolQuest");
            Console.Write("Digite seu nome: ");
            name = Console.ReadLine();

            Console.WriteLine("Neste mundo voce possui 3 caminhos diferentes para seguir digite qual delas voce deseja seguir");
            Console.WriteLine("[0]Guerreiro\n[1]Ladino\n[2]Mago");

            while (!int.TryParse(Console.ReadLine(), out classe) || classe < 0 || classe > 2)
            {
                Console.WriteLine("Opção inválida. Digite 0, 1 ou 2.");
            }

            switch (classe)
            {
                case 0:
                    player = new Warrior(80, 80, 0, 1, name, 10, 6, inventory);
                    break;

                case 1:
                    player = new Rogue(60, 60, 0, 1, name, 15, 3, inventory);
                    break;

                case 2:
                    Spells spell = Spells.Fireball; //Talvez fazer a primeira magia ser aleatoria

                    player = new Mage(50, 50, 0, 1, name, 16, 2, inventory, spell);
                    break;
            }
        }
    }
}
