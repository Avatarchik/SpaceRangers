using System.Collections;
using AstronomicalObject;
using UnityEngine;
using Random = UnityEngine.Random;

public class Npc : MonoBehaviour, ITakeDamage, IObjectData, ICanBeFollowed
{
    [SerializeField] private NpcPath path;
    [SerializeField] private GameObject movementAnchor;
    [SerializeField] private GameObject damageText;
    [SerializeField] private GameObject container;

    private ShipData shipData;
    private GameManager gameManager;
    private GameObject target;
    private float t;
    private int hitPoints;
    private Animator animator;

    private Vector3 targetPos;
    private Vector3 selfPos;
    private static readonly int DestroyTrigger = Animator.StringToHash("Destroy");

    private void Start()
    {
        shipData = ShipDataFactory.GetNpcDefaultShipData();
        hitPoints = shipData.Capacity;
        animator = GetComponentInChildren<Animator>();
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

        if (transform.position != targetPos)
        {
            Vector3 vectorToTarget = targetPos - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5f);

            t += Time.deltaTime / GameManager.TurnDuration;
            var oneTurnRange = Helper
                .CalculateOneTurnDistance(selfPos, targetPos, shipData.Engine.Speed * 0.01f);
            transform.position = Vector3.Lerp(selfPos, oneTurnRange, t);
        }
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
        var oneTurnRange = Helper
            .CalculateOneTurnDistance(selfPos, targetPos, shipData.Engine.Speed * 0.01f);
        path.DrawPath(selfPos, targetPos, oneTurnRange);
    }

    private void OnMouseExit()
    {
        path.DestroyPath();
    }

    private IEnumerator DestructionProcess()
    {
        var obj = transform.Find("Player Target");
        if (obj != null)
        {
            obj.transform.parent = null;
        }
        
        DropLoot();

        animator.SetTrigger(DestroyTrigger);
        while (!animator.GetCurrentAnimatorStateInfo(0).IsName("ExplosionFinished"))
        {
            yield return null;
        }

        Destroy(gameObject);
    }

    public void TakeDamage(int damage)
    {
        var objectPosition = transform.position;
        var textPosition = new Vector3(objectPosition.x, objectPosition.y + 0.3f, objectPosition.z);
        var text = Instantiate(damageText, textPosition, Quaternion.identity, transform);
        text.GetComponent<TextMesh>().text = damage.ToString();
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            StartCoroutine(DestructionProcess());
        }
    }

    public string GetObjectData()
    {
        return $"Structure: {hitPoints}/{shipData.Capacity}\nSpeed: {shipData.Engine.Speed}";
    }

    private void DropLoot()
    {
        var itemToDrop = shipData.GetRandomItemFromShip();
        var containerObject = Instantiate(container, transform.position, Quaternion.identity);
        containerObject.name = itemToDrop.Name;
        containerObject.GetComponent<Container>().Content = itemToDrop;
    }
}