using UnityEngine;

public class PlayerPath : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject target;
    private GameManager gameManager;
    private PathBuilder pathBuilder;
    private Vector3 lastTargetPos;
    public bool redrawPath;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        pathBuilder = GetComponentInChildren<PathBuilder>();
        lastTargetPos = target.transform.position;
    }

    private void Update()
    {
        if (gameManager.TurnInProgress)
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
                player.GetComponent<PlayerController>().OneTurnRange);
            redrawPath = false;
        }
    }
}