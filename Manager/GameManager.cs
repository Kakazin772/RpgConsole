using ConsolQuest;
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
        private Generate generates;

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

            generates = new Generate(player, Dungeon);

            generates.GenerateDungeon();
            Combat = new CombatManager();

            for (int i = 0; i < 6; i++)
            {
                bool avancar = false;

                while (!avancar)
                {
                    Console.WriteLine($"\n--- {player.Name} | Vida: {player.ActualLife}/{player.life} | Nível: {player.level} | Tochas: {player.Inventory.torches} ---");
                    Console.WriteLine("[1] Avançar para a próxima sala");
                    Console.WriteLine("[2] Abrir Inventário");

                    if (!int.TryParse(Console.ReadLine(), out int opcao) || (opcao != 1 && opcao != 2))
                    {
                        Console.WriteLine("Opção inválida.");
                        continue;
                    }

                    if (opcao == 1)
                    {
                        avancar = true;
                    }
                    else
                    {
                        OpenInventory();
                    }
                }

                player.Inventory.ConsumeTorch();

                if (player.Inventory.torches <= 0)
                {
                    ProcessGameOver(false);
                }

                ExploreRoom(Dungeon[i]);

                if (player.ActualLife <= 0 || player.Inventory.torches <= 0)
                {
                    break;
                }
            }
        }

        private void ExploreRoom(Room room)
        {
            switch (room.roomType)
            {
                case RoomType.Chest:
                    Console.WriteLine($"Voce encontrou um baú! e dentro dele havia {room.roomItem.Name}");
                    player.Inventory.AddItem(room.roomItem);
                    break;

                case RoomType.Event:
                    generates.ExecuteRandomEvent();
                    break;

                case RoomType.Empty:
                    Console.WriteLine("Ao entrar na sala voce percebe q ela estava vazia!");
                    break;

                case RoomType.Enemy:
                    CombatResult result = Combat.StartCombat(player, room.roomEnemy);

                    if (result == CombatResult.Defeat)
                    {
                        ProcessGameOver(false);
                    }

                    if (result == CombatResult.Victory)
                    {
                        player.GainXp(room.roomEnemy.XpReward);
                        MonsterDefeated++;
                        Console.WriteLine($"Você derrotou {room.roomEnemy.Name} e ganhou {room.roomEnemy.XpReward} de XP!");

                        if (room.roomEnemy is Boss)
                        {
                            ProcessGameOver(true);
                        }
                        break;
                    }

                    break;
            }

            room.Visited = true;
        }

        public void OpenInventory()
        {
            bool sair = false;

            while (!sair)
            {
                player.Inventory.ViewInventory();

                Console.WriteLine("[1] Equipar item");
                Console.WriteLine("[2] Usar poção");
                Console.WriteLine("[3] Voltar");

                if (!int.TryParse(Console.ReadLine(), out int opcao) || opcao < 1 || opcao > 3)
                {
                    Console.WriteLine("Opção inválida.");
                    continue;
                }

                switch (opcao)
                {
                    case 1:
                        Console.WriteLine("Digite o numero do item desejado:");

                        if (!int.TryParse(Console.ReadLine(), out int itemOption) || itemOption < 0 || itemOption >= player.Inventory.items.Count)
                        {
                            Console.WriteLine("Opção inválida.");
                            continue;
                        }

                        try
                        {
                            player.Inventory.EquipItem(player.Inventory.items[itemOption]);
                            Console.WriteLine("Item equipado!");
                        }
                        catch (GameException ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                        break;

                    case 2:
                        Console.WriteLine("Digite o numero da poçao desejada:");

                        if (!int.TryParse(Console.ReadLine(), out int potionOption) || potionOption < 0 || potionOption >= player.Inventory.items.Count)
                        {
                            Console.WriteLine("Opção inválida.");
                            continue;
                        }

                        if (player.Inventory.items[potionOption] is not Potion)
                        {
                            Console.WriteLine("Este Item nao é uma poção");
                            continue;
                        }

                        try
                        {
                            int cura = player.Inventory.UsePotion(player.Inventory.items[potionOption] as Potion);

                            player.HealDamage(cura);
                            Console.WriteLine($"Você recuperou {cura} de vida!");
                        }
                        catch (GameException e)
                        {
                            Console.WriteLine(e.Message);
                        }

                        break;

                    case 3:
                        sair = true;
                        break;
                }
            }
        }

        private void ProcessGameOver(bool victory)
        {
            Console.WriteLine("\n=============================");

            if (victory)
            {
                Console.WriteLine("           VITÓRIA!");
                Console.WriteLine("Você derrotou o Chefe da Dungeon!");
            }
            else
            {
                Console.WriteLine("          GAME OVER");
                Console.WriteLine("Sua jornada chegou ao fim...");
            }

            Console.WriteLine("=============================");
            Console.WriteLine($"Nível final alcançado: {player.level}");
            Console.WriteLine($"Monstros derrotados: {MonsterDefeated}");
            Console.WriteLine("=============================");

            Console.WriteLine("\nPressione qualquer tecla para continuar...");
            Console.ReadKey();

            Environment.Exit(0);
        }
    }
}
