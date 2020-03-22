namespace Equipment
{
    public class Grab : Item
    {
        public int Range { get; }
        public int Power { get; }

        public Grab(int weight, int range, int power) : base(weight)
        {
            Range = range;
            Power = power;
        }
    }
}