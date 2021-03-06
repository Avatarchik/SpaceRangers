﻿namespace Equipment
{
    public class Engine : Item
    {
        public int Speed { get; }

        public Engine(string name, int weight, int speed) : base(name, weight)
        {
            Speed = speed;
        }

        public override string ToString()
        {
            return base.ToString() + $"Speed: {Speed}";
        }
    }
}