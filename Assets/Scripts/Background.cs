using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private GameObject cam;
    [SerializeField] private float parallaxEffect;

    private float startPos;
    
    private void Start()
    {
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