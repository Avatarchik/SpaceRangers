using UnityEngine;

namespace Player
{
    public class PlayerPath : MonoBehaviour
    {
        [SerializeField] private GameObject target;
        [SerializeField] private GameObject pathTextPrefab;

        private GameObject pathText;
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
                Destroy(pathText);
                pathBuilder.DestroyAllDots();
                redrawPath = true;
                return;
            }

            if (target != null && lastTargetPos != target.transform.position)
            {
                redrawPath = true;
            }

            if (player.transform.position != target.transform.position && redrawPath)
            {
                Destroy(pathText);
                DrawPath();
            }
        }

        private void DrawPath()
        {
            lastTargetPos = target.transform.position;
            var playerPos = player.transform.position;
            var speed = player.GetComponent<PlayerController>().ShipData.Engine.Speed * 0.01f;
            var oneTurnDistance = Helper.CalculateOneTurnDistance(playerPos, lastTargetPos, speed);
            pathBuilder.DrawDottedLine(playerPos, lastTargetPos, oneTurnDistance);
            redrawPath = false;

            var textShift = playerPos.y > lastTargetPos.y ? -0.1f : 0.2f;
            var textPosition = new Vector3(lastTargetPos.x, lastTargetPos.y + textShift, lastTargetPos.z);
            pathText = Instantiate(pathTextPrefab, textPosition, Quaternion.identity, transform);
            pathText.GetComponent<TextMesh>().text = GetPathInfoText(playerPos, oneTurnDistance);
        }

        private string GetPathInfoText(Vector3 playerPos, Vector3 oneTurnDistance)
        {
            var pathLength = Vector3.Distance(playerPos, lastTargetPos);
            var oneTurnLength = Vector3.Distance(playerPos, oneTurnDistance);
            var pathLengthInTurns = pathLength / oneTurnLength;
            var intPathLengthInTurns = (int) (pathLengthInTurns > 1 ? pathLengthInTurns + 1 : pathLengthInTurns);
            var pathLengthFriendly = (int) (pathLength * 100);
            return $"{intPathLengthInTurns} ({pathLengthFriendly})";
        }
    }
}