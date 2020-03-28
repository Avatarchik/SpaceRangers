using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CircleDrawer
{
    private const int Segments = 360;

    public static void DrawCircle(GameObject obj, float radius, Color color)
    {
        var line = obj.AddComponent<LineRenderer>();
        line.useWorldSpace = false;
        line.material = new Material(Shader.Find("Sprites/Default"));
        line.startColor = color;
        line.endColor = color;
        line.startWidth = 0.01f;
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