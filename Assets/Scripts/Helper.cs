using UnityEngine;

public static class Helper
{
    public static Vector3 CalculateOneTurnDistance(Vector3 start, Vector3 end, float speed)
    {
        var pathDistance = Vector3.Distance(end, start);
        return Vector3.Lerp(start, end, speed / pathDistance);
    }
}