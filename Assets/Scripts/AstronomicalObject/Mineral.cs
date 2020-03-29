namespace AstronomicalObject
{
    public class Mineral : SpaceItem, IObjectData
    {
        public string GetObjectData()
        {
            return $"Amount: {Content.Weight}";
        }
    }
}