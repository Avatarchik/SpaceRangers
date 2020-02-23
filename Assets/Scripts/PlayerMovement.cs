using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GameObject target;

    public float Speed { get; set; } = 1f;

    private PlayerMovement player;
    private GameManager gameManager;
    private Vector2 targetPos;
    
    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        targetPos = transform.position;
    }

    private void Update()
    {
        bool turnInProgress = gameManager.TurnInProgress;
        if (!turnInProgress)
        {
            GetTargetPosition();
        }

        if (turnInProgress)
        {
            if (target.transform.parent != null)
            {
                targetPos = target.transform.position;
            }
            Move(targetPos);
        }
    }

    private void GetTargetPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
            //
            RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector2.zero);
            if (hit.collider != null)
            {
                target.transform.parent = hit.transform;
            }

            //
            else
            {
                target.transform.parent = null;
            }
            target.transform.position = targetPos;

            float dist = Vector3.Distance(targetPos, transform.position);
            Debug.Log("Distance to target: " + dist);
        }
    }

    private void Move(Vector2 targetPos)
    {
        if ((Vector2) transform.position != targetPos)
        {
            Vector3 vectorToTarget = (Vector3) targetPos - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5f);

            transform.position = Vector2.MoveTowards(transform.position, targetPos, Speed * Time.deltaTime);
        }
    }
}