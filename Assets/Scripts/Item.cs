using UnityEngine;

namespace DefaultNamespace
{
    public abstract class Item : MonoBehaviour
    {
        public int Weight { get; protected set; }
    }
}