using System.Collections.Generic;
using System.Linq;
using Equipment;
using UnityEngine;

public class Ship : MonoBehaviour
{
    //TODO: refactor all
    public int Capacity { get; set; }
    public List<Weapon> Weapons { get; set; }
    public Grab Grab { get; set; }
    public List<Item> Cargo { get; set; }
    public int Money { get; set; }
    public float Speed { get; set; }

    private void Awake()
    {
        Speed = 4f;
        Capacity = 250;
        Cargo = new List<Item>();
        Weapons = new List<Weapon>
        {
            new Weapon(30, 15, 150)
        };
        Grab = new Grab(30, 1, 40);
        Money = 5000;
    }

    public int GetFreeSpace()
    {
        var weaponWeight = Weapons.Sum(item => item.Weight);
        var cargoWeight = Cargo.Sum(item => item.Weight);
        return Capacity - weaponWeight - cargoWeight - Grab.Weight;
    }
}