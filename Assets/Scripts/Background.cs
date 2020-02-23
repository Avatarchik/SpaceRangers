using UnityEngine;

public class Background : MonoBehaviour
{
    private float startPos;
    private GameObject cam;

    [SerializeField] private float parallaxEffect;

    private void Start()
    {
        cam = Camera.main.gameObject;
        startPos = transform.position.x;
    }

    private void Update()
    {
        var camPos = cam.transform.position;
        var shiftX = camPos.x * parallaxEffect;
        var shiftY = camPos.y * parallaxEffect;
        transform.position = new Vector3(startPos + shiftX, startPos + shiftY, 0f);
    }
}