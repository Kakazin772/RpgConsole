using ConsolQuest;
using ConsolQuest.Equipments;
using System;
using System.Collections.Generic;
using System.Text;

namespace Player
{
    class Mage : Player
    {
        private HashSet<Spells> _spells = new HashSet<Spells>();
        private readonly Random random = new Random();

        public Mage (int life, int actualLife, int xp, int level, string name, int damage, int defense, Inventory inventory, Spells spel1) : base(life, actualLife, xp, level, name, damage, defense, inventory)
        {
            _spells.Add(spel1);
        }

        public override void HealDamage(int amount)
        {
            if (ActualLife == life)
            {
                throw new GameException("Sua vida ja esta cheia");
            }

            ActualLife = Math.Min(ActualLife + amount, life);
        }

        public override void TakeDamage(int amount)
        {
            ActualLife = Math.Max(ActualLife -  amount, 0);
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

        public override void GainXp(int amount)
        {
            Xp += amount;

            while (Xp >= XpNecessarioProximoNivel())
            {
                Xp -= XpNecessarioProximoNivel();
                LevelUp();
            }
        }
    }
}
