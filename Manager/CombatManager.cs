using ConsolQuest;
using Equipments;
using Enemys;
using Playerr;
using System;

namespace Manager
{
    class CombatManager
    {
        private Enemy enemy;
        private Player player;
        private Random random = new Random();

        private int defenseMultiplier = 1;

        public CombatResult StartCombat(Player player, Enemy enemy)
        {
            this.enemy = enemy;
            this.player = player;

            while(player.ActualLife > 0 && enemy.Life > 0)
            {
                if (!int.TryParse(Console.ReadLine(), out int input) || !Enum.IsDefined(typeof(CombatAction), input))
                {
                    throw new GameException("Açao Invalida");
                }

                CombatAction action = (CombatAction)input;

                if (action == CombatAction.Flee && enemy is Boss)
                {
                    Console.WriteLine("Não é possível fugir do Chefe!");
                    continue;
                }

                CombatResult? result = ExecutePlayerTurn(action);

                if (result != null)
                {
                    return (CombatResult)result;
                }

                ExecuteEnemyTurn();
            }

            return player.ActualLife > 0 ? CombatResult.Victory : CombatResult.Defeat;
        }

        public CombatResult? ExecutePlayerTurn(CombatAction action)
        {
            switch (action)
            {
                case CombatAction.Attack:
                    int weaponBonus = player.Inventory.equippedWeapon?.AttackBonus ?? 0;
                    int damage = CalculateDamage(player.damage + weaponBonus, enemy.Defense);
                    enemy.TakeDamage(damage);
                    Console.WriteLine($"Voce causou {damage} de dano em {enemy.Name}!");

                    return null;

                case CombatAction.Defend:
                    defenseMultiplier = 2;
                    Console.WriteLine("Voce se prepara para se defender");

                    return null;

                case CombatAction.Flee:
                    if (random.Next(2) == 0)
                    {
                        Console.WriteLine("Voce nao conseguiu fugir!");
                        return null;
                    }

                    Console.WriteLine("Voce escapou!");
                    player.Inventory.ConsumeTorch();

                    if (player.Inventory.torches <= 0)
                    {
                        Console.WriteLine("Suas tochas acabaram na fuga!");
                        return CombatResult.Defeat;
                    }

                    return CombatResult.Flee;

                case CombatAction.Special:
                    return ExecuteSpecial(player);

                default:
                    return null;
            }
        }

        private void ExecuteEnemyTurn()
        {
            int attackValue = enemy is Boss boss ? boss.SpecialAttack() : enemy.Attack;
            int shieldBonus = player.Inventory.equippedShield?.DefenseBonus ?? 0;
            player.TakeDamage(CalculateDamage(attackValue, (player.defense + shieldBonus) * defenseMultiplier));
            defenseMultiplier = 1;
        }

        public int CalculateDamage(int attackerDamage, int defenserDefense)
        {
            return Math.Max(attackerDamage - defenserDefense, 1);
        }

        private CombatResult? ExecuteSpecial(Player player)
        {
            if (player is Mage)
            {
                Mage mage = (Mage)player;

                Console.WriteLine("Digite qual magia vc deseja utilizar?");

                foreach (Spells spells in mage.SpellsList)
                {
                    Console.WriteLine($"[{(int)spells}] {spells}");
                }

                int spell;

                while (!int.TryParse(Console.ReadLine(), out spell) || !Enum.IsDefined(typeof(Spells), spell))
                {
                    Console.WriteLine("Opção inválida. Digite o número de uma magia da lista.");
                }

                Spells spellDecide = (Spells)spell;

                int value = mage.CastSpell(spellDecide);

                if (spellDecide == Spells.Regrowth)
                {
                    player.HealDamage(value);
                    Console.WriteLine($"Voce recuperou {value} de vida!");
                }
                else
                {
                    enemy.TakeDamage(CalculateDamage(value, enemy.Defense));
                    Console.WriteLine($"Voce causou {value} de dano em {enemy.Name}!");
                }

                return null;
            }

            if (player is Rogue)
            {
                if (enemy is Boss)
                {
                    Console.WriteLine("Não é possível fugir do Chefe, nem com Fuga Especial!");
                    return null;
                }

                player.Inventory.ConsumeTorch();
                player.Inventory.ConsumeTorch();

                Console.WriteLine("Voce foge com extrema eficacia mas perde 2 tochas no processo");

                if (player.Inventory.torches <= 0)
                {
                    Console.WriteLine("Suas tochas acabaram na fuga!");
                    return CombatResult.Defeat;
                }

                return CombatResult.Flee;
            }

            if (player is Warrior)
            {
                Console.WriteLine("Voce arma a sua super defesa!");
                defenseMultiplier = 3;

                return null;
            }

            return null;
        }
    }
}
