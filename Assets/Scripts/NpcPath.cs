using UnityEngine;

public class NpcPath : MonoBehaviour
{
    private GameManager gameManager;
    private PathBuilder pathBuilder;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        pathBuilder = GetComponentInChildren<PathBuilder>();
    }

    public void DrawPath(Vector2 npcPosition, Vector2 targetPosition, Vector2 oneTurnRange)
    {
        if (gameManager.TurnInProgress)
        {
            return;
        }

        pathBuilder.DrawDottedLine(npcPosition, targetPosition, oneTurnRange);
    }

    public void DestroyPath()
    {
        pathBuilder.DestroyAllDots();
    }
}