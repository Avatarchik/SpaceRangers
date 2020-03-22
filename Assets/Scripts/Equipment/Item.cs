namespace Equipment
{
    public abstract class Item
    {
        public int Weight { get;}

        protected Item(int weight)
        {
            Weight = weight;
        }
    }
}