using ConsolQuest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Playerr
{
    abstract class Player
    {
        public int life { get; protected set; }
        public int ActualLife { get; set; }
        public int Xp { get; protected set; }
        public int level { get; protected set; }
        public string Name { get; protected set; }
        public int damage { get; protected set; }
        public int defense { get; protected set; }
        public Inventory Inventory { get; protected set; }

        public Player()
        {
        }

        public Player(int life, int actualLife, int xp, int level, string name, int damage, int defense, Inventory inventory)
        {
            this.life = life;
            ActualLife = actualLife;
            Xp = xp;
            this.level = level;
            Name = name;
            this.damage = damage;
            this.defense = defense;
            Inventory = inventory;
        }
        protected void IncreaseBaseAttributes(int lifeGain, int damageGain, int defenseGain)
        {
            life += lifeGain;
            damage += damageGain;
            defense += defenseGain;
            ActualLife = life;
            level++;
        }

        protected virtual int XpNecessarioProximoNivel()
        {
            return level * 10;
        }

        public void TakeDamage(int amount)
        {
            ActualLife = Math.Max(ActualLife - amount, 0);
        }

        public void HealDamage(int amount)
        {
            if (ActualLife == life)
            {
                throw new GameException("Sua vida ja esta cheia");
            }

            ActualLife = Math.Min(ActualLife + amount, life);
        }

        public void GainXp(int amount)
        {
            Xp += amount;

            while (Xp >= XpNecessarioProximoNivel())
            {
                Xp -= XpNecessarioProximoNivel();
                LevelUp();
            }
        }

        public abstract void LevelUp();
    }
}
