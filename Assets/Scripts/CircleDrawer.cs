using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CircleDrawer
{
    private const int Segments = 360;

    public static void DrawCircle(GameObject obj, float radius, float lineWidth)
    {
        var line = obj.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.startColor = Color.red;
        line.startWidth = lineWidth;
        line.positionCount = Segments + 1;

        var pointCount = Segments + 1;
        var points = new Vector3[pointCount];

        for (int i = 0; i < pointCount; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / Segments);
            points[i] = new Vector3(Mathf.Sin(rad) * radius, Mathf.Cos(rad) * radius, 0);
        }

        line.SetPositions(points);
    }
}