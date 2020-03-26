using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Npc : MonoBehaviour, ITakeDamage, IObjectData
{
    [SerializeField] private NpcPath path;
    [SerializeField] private GameObject movementAnchor;

    private GameManager gameManager;
    private GameObject target;
    private float t;
    private float speed = 3.5f;
    private int maxHitPoints = 100;
    private int hitPoints;
    private Animator animator;

    private Vector2 targetPos;
    private Vector2 selfPos;
    public Vector2 oneTurnRange;
    private static readonly int DestroyTrigger = Animator.StringToHash("Destroy");

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
        hitPoints = maxHitPoints;
        target = new GameObject("Npc target");
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
            CalculateOneTurnDistance();
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
        if (hitPoints <= 0)
        {
            return;
        }
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
        var anchorPosition = movementAnchor.transform.position;
        var targetX = Random.Range(anchorPosition.x - 3f, anchorPosition.y + 5f);
        var targetY = Random.Range(anchorPosition.x - 3f, anchorPosition.y + 5f);
        target.transform.position = new Vector3(targetX, targetY, 0f);
        targetPos = target.transform.position;
    }

    private void OnMouseEnter()
    {
        path.DrawPath(transform.position, target.transform.position, oneTurnRange);
    }

    private void OnMouseExit()
    {
        path.DestroyPath();
    }
    
    private IEnumerator DestructionProcess()
    {
        animator.SetTrigger(DestroyTrigger);
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("ExplosionFinished"))
        {
            yield return null;
        }
        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            StartCoroutine(DestructionProcess());
        }
    }

    public string GetObjectData()
    {
        return $"Structure: {hitPoints}/{maxHitPoints}\nSpeed: {speed}";
    }
}