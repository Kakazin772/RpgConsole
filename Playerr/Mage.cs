using ConsolQuest;
using Equipments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Playerr
{
    class Mage : Player
    {
        private HashSet<Spells> _spells = new HashSet<Spells>();
        public IReadOnlyCollection<Spells> SpellsList => _spells;
        private readonly Random random = new Random();

        public Mage (int life, int actualLife, int xp, int level, string name, int damage, int defense, Inventory inventory, Spells spel1) : base(life, actualLife, xp, level, name, damage, defense, inventory)
        {
            _spells.Add(spel1);
        }

        public override void LevelUp()
        {
            IncreaseBaseAttributes(lifeGain: 5, damageGain: 1, defenseGain: 1);

            if (level % 2 == 1)
            {
                List<Spells> disponiveis = new List<Spells>();

                foreach (Spells spell in Enum.GetValues<Spells>())
                {
                    if (!_spells.Contains(spell))
                    {
                        disponiveis.Add(spell);
                    }
                }

                if (disponiveis.Count == 0)
                {
                    Console.WriteLine("Você já aprendeu todas as magias.");
                    return;
                }

                Spells spellSorteada = disponiveis[random.Next(disponiveis.Count)];
                _spells.Add(spellSorteada);

                Console.WriteLine($"Você aprendeu {spellSorteada}!");
            }
        }

        public int CastSpell(Spells spell)
        {
            switch (spell)
            {
                case Spells.Fireball:
                    return damage + 5;

                case Spells.EletricShot:
                    return damage + 3;

                case Spells.SparkGap:
                    return damage + 8;

                case Spells.IceBeam:
                    return damage + 6;

                case Spells.Regrowth:
                    return 15;

                default:
                    return 0;
            }
        }
    }
}
