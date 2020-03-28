using System.Collections.Generic;
using System.Linq;
using Equipment;
using UnityEngine;

public class ShipData
{
    public int Capacity { get; set; }
    public List<Weapon> Weapons { get; } = new List<Weapon>();
    public List<Item> Cargo { get; } = new List<Item>();
    public Grab Grab { get; set; }
    public Engine Engine { get; set; }
    public int Money { get; set; }

    public int GetFreeSpace()
    {
        var weaponWeight = Weapons.Sum(item => item.Weight);
        var cargoWeight = Cargo.Sum(item => item.Weight);
        return Capacity - weaponWeight - cargoWeight - Grab.Weight - Engine.Weight;
    }
}