namespace Equipment
{
    public class Weapon : Item
    {
        public int MinDamage { get; }
        public int MaxDamage { get; }
        public int Range { get; }

        public Weapon(string name, int weight, int minDamage,int maxDamage, int range) : base(name, weight)
        {
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            Range = range;
        }
        
        public override string ToString()
        {
            return base.ToString() + $"Damage: {MinDamage}-{MaxDamage}\nRange: {Range}";
        }
    }
}