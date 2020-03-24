using System.Collections.Generic;
using System.Linq;
using Equipment;
using UnityEngine;

public class Ship : MonoBehaviour
{
    //TODO: refactor and move creation responsibility outside
    public int Capacity { get; set; }
    public List<Weapon> Weapons { get; set; }
    public Grab Grab { get; set; }
    public Engine Engine { get; set; }
    public List<Item> Cargo { get; set; }
    public int Money { get; set; }
    public float Speed { get; set; }

    private void Awake()
    {
        Cargo = new List<Item>();
        Weapons = new List<Weapon>
        {
            new Weapon("Laser", 30, 15, 150)
        };
        Grab = new Grab("Simple Grab",30, 1, 40);
        Engine = new Engine("Simple Engine", 30, 400);
        Money = 5000;
        Capacity = 250;
        Speed = Engine.Speed / 100f;

    }

    public int GetFreeSpace()
    {
        var weaponWeight = Weapons.Sum(item => item.Weight);
        var cargoWeight = Cargo.Sum(item => item.Weight);
        return Capacity - weaponWeight - cargoWeight - Grab.Weight - Engine.Weight;
    }
}