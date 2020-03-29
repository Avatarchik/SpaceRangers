using Equipment;
using UnityEngine;

namespace AstronomicalObject
{
    public class Container : SpaceItem, IObjectData
    {
        public string GetObjectData()
        {
           return Content.ToString();
        }
    }
}