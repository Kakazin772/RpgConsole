using Equipments;
using Enemys;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon
{
    class Room
    {
        public int Id { get; private set; }
        public bool Visited { get; private set; }

        public RoomType roomType { get; private set; }
        public Item roomItem { get; set; }
        public Enemy roomEnemy { get; set; }

        public Room()
        {
        }

        public Room(int id, RoomType type)
        {
            Id = id;
            roomType = type;
        }
    }
}
