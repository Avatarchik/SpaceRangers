using UnityEngine;

public class Path : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject target;
    private GameManager gameManager;
    private PathBuilder pathBuilder;
    private Vector3 lastTargetPos;
    public bool redrawPath;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        pathBuilder = GetComponentInChildren<PathBuilder>();
        lastTargetPos = target.transform.position;
    }

    public void Update()
    {
        bool turnInProgress = gameManager.TurnInProgress;
        if (turnInProgress)
        {
            pathBuilder.DestroyAllDots();
            redrawPath = true;
            return;
        }
        if (lastTargetPos != target.transform.position || redrawPath)
        {
            lastTargetPos = target.transform.position;
            pathBuilder.DrawDottedLine(
                player.transform.position,
                target.transform.position,
                player.GetComponent<PlayerMovement>().oneTurnRange);
            redrawPath = false;
        }
    }
}