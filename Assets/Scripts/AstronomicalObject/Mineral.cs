namespace AstronomicalObject
{
    public class Mineral : SpaceItem, IObjectData
    {
        public int Amount { get; set; }
        public int Weight { get; private set; }

        public string GetObjectData()
        {
            return $"Amount: {Amount}";
        }
    }
}