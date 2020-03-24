namespace Equipment
{
    public class Grab : Item
    {
        public int Range { get; }
        public int Power { get; }

        public Grab(string name, int weight, int range, int power) : base(name, weight)
        {
            Range = range;
            Power = power;
        }
    }
}