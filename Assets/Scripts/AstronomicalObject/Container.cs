using Equipment;
using UnityEngine;

namespace AstronomicalObject
{
    public class Container : SpaceItem, IObjectData
    {
        public Item Content { get; set; }
        
        public string GetObjectData()
        {
           return Content.ToString();
        }
    }
}