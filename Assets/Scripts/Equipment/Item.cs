using System;

namespace Equipment
{
    public class Item
    {
        public int Weight { get; }
        public string Name { get; }

        public Item(string name, int weight)
        {
            Name = name;
            Weight = weight;
        }

        public override string ToString()
        {
            return $"Weight: {Weight}\n";
        }
    }
}