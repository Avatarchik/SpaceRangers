using UnityEngine;

namespace AstronomicalObject
{
    public class Mineral : MonoBehaviour, IObjectData
    {
        public int Amount { get; set; }
        
        public string GetObjectData()
        {
            return $"Amount: {Amount}";
        }
    }
}