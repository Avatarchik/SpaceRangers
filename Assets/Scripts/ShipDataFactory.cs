using Equipment;

public static class ShipDataFactory
{
    public static ShipData GetPlayerDefaultShipData()
    {
        var shipData = new ShipData();
        shipData.Weapons.Add(new Weapon("Laser", 30, 15, 150, 150));
        shipData.Grab = new Grab("Simple Grab", 30, 1, 40);
        shipData.Engine = new Engine("Simple Engine", 30, 400);
        shipData.Money = 5000;
        shipData.Capacity = 250;
        return shipData;
    }

    public static ShipData GetNpcDefaultShipData()
    {
        var shipData = GetPlayerDefaultShipData();
        shipData.Engine = new Engine("Legacy Engine", 30, 350);
        shipData.Capacity = 100;
        return shipData;
    }
}