using System;
using System.Collections.Generic;
using System.Text;

namespace Player
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
            return level * 100;
        }

        public abstract void LevelUp();
        public abstract void GainXp(int amount);
        public abstract void TakeDamage(int amount);
        public abstract void HealDamage(int amount);
    }
}
