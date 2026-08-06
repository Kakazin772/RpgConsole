using ConsolQuest;
using ConsolQuest.Equipments;
using Enemys;
using Playerr;

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

                if (player.Inventory.torches <= 0)
                {
                    Console.WriteLine("Suas tochas acabaram!");

                    return CombatResult.Defeat;
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
                    int damage = CalculateDamage(player.damage, enemy.Defense);
                    enemy.TakeDamage(damage);

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
            player.TakeDamage(CalculateDamage(attackValue, player.defense * defenseMultiplier));
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

                Console.WriteLine("Digita qual magia vc deseja utilizar?");

                foreach (Spells spells in mage.SpellsList)
                {
                    Console.WriteLine($"[{(int)spells}] {spells}");
                }

                if (!int.TryParse(Console.ReadLine(), out int spell) || !Enum.IsDefined(typeof(Spells), spell))
                {
                    throw new GameException("Açao Invalida");
                }

                Spells spellDecide = (Spells)spell;

                int valor = mage.CastSpell(spellDecide);

                if (spellDecide == Spells.Regrowth)
                {
                    player.HealDamage(valor);
                    Console.WriteLine($"Voce recuperou {valor} de vida!");
                }
                else
                {
                    enemy.TakeDamage(valor);
                    Console.WriteLine($"Voce causou {valor} de dano em {enemy.Name}!");
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
