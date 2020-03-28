using UnityEngine;

public class CameraController : MonoBehaviour
{
    private GameManager gameManager;
    private Transform player;
    private float cursorNearBorderThickness = 10f;
    private float speed = 10f;
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        player = GameObject.Find("Player").transform;
    }

    private void Update()
    {
        var pos = transform.position;
        if (gameManager.TurnInProgress)
        {
            var playerPosition = player.transform.position;
            transform.position = new Vector3(playerPosition.x, playerPosition.y, pos.z);
            return;
        }
        /*if (Input.mousePosition.y >= Screen.height - cursorNearBorderThickness)
        {
            pos.y += speed * Time.deltaTime;
        }
        if (Input.mousePosition.y < cursorNearBorderThickness)
        {
            pos.y -= speed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - cursorNearBorderThickness)
        {
            pos.x += speed * Time.deltaTime;
        }
        if (Input.mousePosition.x < cursorNearBorderThickness)
        {
            pos.x -= speed * Time.deltaTime;
        }
        transform.position = pos;*/
    }
}
