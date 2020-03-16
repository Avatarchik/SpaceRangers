using System.Collections.Generic;
using UnityEngine;

public class PathBuilder : MonoBehaviour
{
    [SerializeField] public Sprite greenDot;
    [SerializeField] public Sprite orangeDot;
    [SerializeField] public Sprite oneTurnDistanceDot;
    private float delta = 0.2f;
    private List<GameObject> dots = new List<GameObject>();

    public void DestroyAllDots()
    {
        foreach (var dot in dots)
        {
            Destroy(dot);
        }

        dots.Clear();
    }

    private GameObject GetOneDot(Sprite sprite)
    {
        var obj = new GameObject("Dot");
        obj.transform.parent = transform;

        var sr = obj.AddComponent<SpriteRenderer>();
        sr.sprite = sprite;
        sr.sortingOrder = 5;
        return obj;
    }

    public void DrawDottedLine(Vector2 start, Vector2 end, Vector2 colorChange)
    {
        DestroyAllDots();

        Vector2 point = start;
        Vector2 direction = (end - start).normalized;

        while ((end - start).magnitude > (point - start).magnitude)
        {
            var sprite = (point - start).magnitude > (colorChange - start).magnitude ? orangeDot : greenDot;

            var dot = GetOneDot(sprite);
            dot.transform.position = point;
            dots.Add(dot);
            point += (direction * delta);
        }

        var oneTurnDot = GetOneDot(oneTurnDistanceDot);
        oneTurnDot.transform.position = colorChange;
        dots.Add(oneTurnDot);
    }
}