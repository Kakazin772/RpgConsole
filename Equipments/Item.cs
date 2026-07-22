using System;
using System.Collections.Generic;
using System.Text;

namespace Equipments
{
    abstract class Item
    {
        public string Name { get; protected set; }

        public Item()
        {
        }

        public Item(string name)
        {
            Name = name;
        }
    }
}
