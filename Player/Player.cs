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


        protected abstract void LevelUp();
        public abstract void GainXp(int amount);
        public abstract void TakeDamage(int amount);
        public abstract void HealDamage(int amount);
    }
}
