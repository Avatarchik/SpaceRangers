using System;

namespace Equipment
{
    public abstract class Item
    {
        public int Weight { get; }
        public string Name { get; }

        protected Item(string name, int weight)
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