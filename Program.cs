using System;
using Equipments;
using Playerr;
using Manager;

namespace ConsolQuest
{
    class Program
    {
        static void Main(string[] args)
        {
            GameManager game = new GameManager();

            game.StartGame();
        }
    }
}