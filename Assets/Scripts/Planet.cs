using UnityEngine;

public class Planet : MonoBehaviour
{
    [SerializeField] private int rotationDuration;
    private GameManager gameManager;
    private GameObject star;
    private Vector3 target;

    private void Start()
    {
        star = GameObject.Find("Star");
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (gameManager.TurnInProgress)
        {
            transform.RotateAround(star.transform.position, star.transform.forward,
                -120f / rotationDuration  * Time.deltaTime);
            transform.localRotation = Quaternion.identity;
        }
    }
}