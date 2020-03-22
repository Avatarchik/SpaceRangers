namespace Equipment
{
    public class Weapon : Item
    {
        public int Damage { get; }
        public int Range { get; }

        public Weapon(int weight, int damage, int range) : base(weight)
        {
            Damage = damage;
            Range = range;
        }
    }
}