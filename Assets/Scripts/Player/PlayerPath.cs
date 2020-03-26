using UnityEngine;

namespace Player
{
    public class PlayerPath : MonoBehaviour
    {
        [SerializeField]private GameObject target;
        
        private GameObject player;
        private GameManager gameManager;
        private PathBuilder pathBuilder;
        private Vector3 lastTargetPos;
        public bool redrawPath;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
            pathBuilder = GetComponentInChildren<PathBuilder>();
            player = transform.parent.gameObject;
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
        
            if (lastTargetPos != target.transform.position)
            {
                redrawPath = true;
            }

            if (player.transform.position != target.transform.position && redrawPath)
            {
                DrawPath();
            }
        }

        private void DrawPath()
        {
            lastTargetPos = target.transform.position;
            var playerPos = player.transform.position;
            var oneTurnDistance = player.GetComponent<PlayerController>()
                .CalculateOneTurnDistance(playerPos, lastTargetPos);
            pathBuilder.DrawDottedLine(playerPos, lastTargetPos, oneTurnDistance);
            redrawPath = false;
        }
    }
}