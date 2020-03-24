namespace Equipment
{
    public class Weapon : Item
    {
        public int Damage { get; }
        public int Range { get; }

        public Weapon(string name, int weight, int damage, int range) : base(name, weight)
        {
            Damage = damage;
            Range = range;
        }
    }
}