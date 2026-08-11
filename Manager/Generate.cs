using ConsolQuest.Equipments;
using Dungeon;
using Enemys;
using Equipments;
using Playerr;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsolQuest.Manager
{
    internal class Generate
    {
        public Player player { get; private set; }
        public List<Room> Dungeon { get; private set; } = new List<Room>();
        private readonly Random random = new Random();

        public Generate(Player player, List<Room> dungeon)
        {
            this.player = player;
            Dungeon = dungeon;
        }

        public void GenerateDungeon()
        {
            int i;

            for (i = 0; i < 5; i++)
            {
                Room room;
                RoomType type = GenerateRoomType();

                room = new Room(i + 1, type);

                if (room.roomType == RoomType.Empty)
                {
                    Dungeon.Add(room);
                    continue;
                }

                if (room.roomType == RoomType.Enemy)
                {
                    room.roomEnemy = GenerateEnemy(RoomLevel(room.Id));
                    Dungeon.Add(room);
                    continue;
                }

                if (room.roomType == RoomType.Chest)
                {
                    room.roomItem = GenerateItem(RoomLevel(room.Id));
                    Dungeon.Add(room);
                    continue;
                }

                Dungeon.Add(room);
            }
        }

        private static readonly (string name, int life, int attack, int defense, int xp, int level)[] enemyTemplates = new[]
        {
            // Nível 1
            ("Rato Gigante", 25, 8, 1, 20, 1),
            ("Morcego das Sombras", 20, 10, 1, 20, 1),
            ("Slime Ácido", 30, 7, 2, 25, 1),
            ("Goblin Saqueador", 28, 9, 2, 25, 1),
            ("Lobo Selvagem", 32, 11, 2, 30, 1),
            ("Esqueleto Guerreiro", 25, 12, 3, 30, 1),
            ("Zumbi Lento", 40, 8, 1, 30, 1),

            // Nível 2
            ("Aranha Peçonhenta", 35, 14, 2, 40, 2),
            ("Bandido Desonesto", 45, 13, 3, 40, 2),
            ("Cultista Obscuro", 38, 15, 2, 45, 2),
            ("Orc Furioso", 55, 14, 4, 45, 2),
            ("Espectro Errante", 30, 16, 5, 50, 2),
            ("Golem de Barro", 65, 10, 6, 50, 2),
            ("Centauro Corrompido", 50, 15, 4, 55, 2),

            // Nível 3
            ("Gárgula de Pedra", 55, 14, 8, 60, 3),
            ("Mago Sombrio", 45, 18, 3, 60, 3),
            ("Cavaleiro Caído", 65, 17, 6, 65, 3),
            ("Troll das Cavernas", 80, 16, 4, 70, 3),
            ("Vampiro Menor", 50, 19, 4, 70, 3),
            ("Minotauro", 75, 20, 5, 80, 3),
        };

        private int RoomLevel(int roomId)
        {
            if (roomId <= 2) return 1;
            if (roomId <= 4) return 2;
            return 3;
        }

        private Enemy GenerateEnemy(int nivelDungeon)
        {
            var candidatos = enemyTemplates.Where(t => t.level == nivelDungeon).ToList();
            var escolhido = candidatos[random.Next(candidatos.Count)];
            return new Enemy(escolhido.name, escolhido.life, escolhido.attack, escolhido.defense, escolhido.xp, escolhido.level);
        }

        List<Item> lootPool = new List<Item>
        {
            // ARMAS (Weapons)
    
            // Nível 1
            new Weapon("Adaga Enferrujada", 1, 2),
            new Weapon("Espada Curta", 1, 4),
            new Weapon("Porrete de Madeira", 1, 3),
    
            // Nível 2
            new Weapon("Machado de Batalha", 2, 8),
            new Weapon("Espada Longa de Aço", 2, 10),
            new Weapon("Lança Perfurante", 2, 9),
    
            // Nível 3
            new Weapon("Montante do Cavaleiro", 3, 15),
            new Weapon("Martelo Destruidor", 3, 18),
            new Weapon("Lâmina das Sombras", 3, 22),

            // ESCUDOS (Shields)
    
            // Nível 1
            new Shield("Tampa de Barril", 1, 1),
            new Shield("Escudo de Madeira Gasto", 1, 2),
            new Shield("Broquel de Couro", 1, 3),
    
            // Nível 2
            new Shield("Escudo de Bronze", 2, 5),
            new Shield("Escudo Pipa de Ferro", 2, 7),
            new Shield("Casco de Tartaruga Gigante", 2, 8),
    
            // Nível 3
            new Shield("Escudo de Torre de Aço", 3, 12),
            new Shield("Escudo Reforçado de Titânio", 3, 15),
            new Shield("Égide do Paladino", 3, 18),

            // POÇÕES (Potions)
    
            // Nível 1
            new Potion("Frasco de Água Revigorante", 1, 15),
            new Potion("Poção Pequena de Cura", 1, 30),
    
            // Nível 2
            new Potion("Poção Média de Cura", 2, 50),
            new Potion("Extrato de Ervas Curativas", 2, 65),
    
            // Nível 3
            new Potion("Poção Grande de Cura", 3, 90),
            new Potion("Elixir da Vida Total", 3, 150)
        };

        private Item GenerateItem(int roomlevel)
        {
            var candidatos = lootPool.Where(t => t.Level == roomlevel).ToList();
            var escolhido = candidatos[random.Next(candidatos.Count)];

            switch (escolhido)
            {
                case Weapon w:
                    return new Weapon(w.Name, w.Level, w.AttackBonus);

                case Shield s:
                    return new Shield(s.Name, s.Level, s.DefenseBonus);

                case Potion p:
                    return new Potion(p.Name, p.Level, p.HealBonus);

                default:
                    throw new GameException("Tipo de item desconhecido no loot pool.");
            }
        }

        public void ExecuteRandomEvent()
        {
            int eventoSorteado = random.Next(1, 16);

            Console.WriteLine("Você entrou em uma sala misteriosa...");

            switch (eventoSorteado)
            {
                case 1:
                    Console.WriteLine("Evento: Fonte da Vida! Você bebe a água cristalina e se sente revigorado.");

                    try
                    {
                        player.HealDamage(20);
                    }
                    catch (GameException)
                    {
                        Console.WriteLine("Sua vida já estava cheia, o efeito não teve impacto extra.");
                    }

                    break;

                case 2:
                    Console.WriteLine("Evento: Armadilha de Espinhos! Você pisa em um piso falso e é atingido.");
                    player.TakeDamage(15);
                    break;

                case 3:
                    Console.WriteLine("Evento: Altar da Sabedoria! Você lê runas antigas e adquire conhecimento.");
                    player.GainXp(50);
                    break;

                case 4:
                    Console.WriteLine("Evento: Mochila Esquecida! Você encontra suprimentos de um aventureiro do passado.");
                    player.Inventory.AddTorch();
                    break;

                case 5:
                    Console.WriteLine("Evento: Gás Venenoso! A sala está cheia de esporos tóxicos.");
                    player.TakeDamage(10);
                    break;

                case 6:
                    Console.WriteLine("Evento: Bênção Divina! Um feixe de luz desce do teto e cura suas feridas.");

                    try
                    {
                        player.HealDamage(40);
                    }
                    catch (GameException)
                    {
                        Console.WriteLine("Sua vida já estava cheia, o efeito não teve impacto extra.");
                    }

                    break;

                case 7:
                    Console.WriteLine("Evento: Moedas Perdidas! Você encontra um pequeno saco de couro no chão.");
                    player.Inventory.AddGold(25);
                    break;

                case 8:
                    Console.WriteLine("Evento: Ladrão Sorrateiro! Um vulto passa correndo e rouba parte do seu ouro.");
                    player.Inventory.RemoveGold(20);
                    break;

                case 9:
                    Console.WriteLine("Evento: Espírito Ancestral! Um fantasma amigável compartilha sua força vital com você.");
                    player.GainXp(30);

                    try
                    {
                        player.HealDamage(10);
                    }
                    catch (GameException)
                    {
                        Console.WriteLine("Sua vida já estava cheia, o efeito não teve impacto extra.");
                    }

                    break;

                case 10:
                    Console.WriteLine("Evento: Goteira Ácida! Uma gota de ácido cai no seu ombro.");
                    player.TakeDamage(5);
                    break;

                case 11:
                    Console.WriteLine("Evento: Aventureiro Caído! Você saqueia um corpo e encontra itens úteis.");
                    player.Inventory.AddGold(15);
                    break;

                case 12:
                    Console.WriteLine("Evento: Vento Gelado! Uma rajada de vento forte apaga uma de suas tochas.");
                    player.Inventory.ConsumeTorch();
                    break;

                case 13:
                    Console.WriteLine("Evento: Pacto Sombrio! Uma estátua drena sua energia, mas revela segredos sombrios.");
                    player.TakeDamage(15);
                    player.GainXp(40);
                    break;

                case 14:
                    Console.WriteLine("Evento: Teto Falso! Pedras desabam repentinamente sobre sua cabeça.");
                    player.TakeDamage(20);
                    break;

                case 15:
                    Console.WriteLine("Evento: Silêncio Perturbador... A sala está completamente vazia. Absolutamente nada acontece.");
                    break;
            }
        }

        private RoomType GenerateRoomType()
        {
            Array values = Enum.GetValues(typeof(RoomType));
            return (RoomType)values.GetValue(random.Next(values.Length));
        }
    }
}
