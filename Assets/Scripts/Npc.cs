using UnityEngine;
using Random = UnityEngine.Random;

public class Npc : MonoBehaviour
{
    [SerializeField] private GameObject targetPrefab;

    private GameManager gameManager;
    private Vector3 starPosition;
    private GameObject target;
    private float t;
    private float speed = 3.5f;

    private Vector2 targetPos;
    private Vector2 selfPos;
    public Vector2 oneTurnRange;

    private void Start()
    {
        target = Instantiate(targetPrefab);
        starPosition = GameObject.Find("Star").transform.position;
        gameManager = FindObjectOfType<GameManager>();
        FindTargetToMove();
    }

    private void Update()
    {
        bool turnInProgress = gameManager.TurnInProgress;
        if (!turnInProgress)
        {
            t = 0f;
            selfPos = transform.position;
            if (transform.position == target.transform.position)
            {
                FindTargetToMove();
            }
        }

        if (turnInProgress)
        {
            Move();
        }
    }

    private void Move()
    {
        if ((Vector2) transform.position != targetPos)
        {
            Vector3 vectorToTarget = (Vector3) targetPos - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5f);
            
            CalculateOneTurnDistance();

            t += Time.deltaTime / GameManager.TurnDuration;
            transform.position = Vector3.Lerp(selfPos, oneTurnRange, t);
        }
    }
    
    private void CalculateOneTurnDistance()
    {
        var pathDistance = Vector2.Distance(targetPos, selfPos);
        oneTurnRange = Vector3.Lerp(selfPos, targetPos, speed / pathDistance);
    }

    private void FindTargetToMove()
    {
        var targetX = Random.Range(starPosition.x - 3f, starPosition.y + 5f);
        var targetY = Random.Range(starPosition.x - 3f, starPosition.y + 5f);
        target.transform.position = new Vector3(targetX, targetY, 0f);
        targetPos = target.transform.position;
    }
}