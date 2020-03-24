﻿using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour, IObjectData, ITakeDamage
{
    [SerializeField] private GameObject target;
    [SerializeField] private Camera cam;

    private Ship playerShip;
    private int hitPoints;
    private GameManager gameManager;
    private Animator shipAnimator;

    private float t;

    private Vector2 targetPos;
    private Vector2 playerPos;
    private static readonly int DestroyTrigger = Animator.StringToHash("Destroy");
    public Vector2 OneTurnRange { get; private set; }

    private void Start()
    {
        playerShip = GetComponentInChildren<Ship>();
        shipAnimator = GetComponentInChildren<Animator>();
        hitPoints = playerShip.Capacity;
        gameManager = FindObjectOfType<GameManager>();
        targetPos = transform.position;
    }

    private void Update()
    {
        var turnInProgress = gameManager.TurnInProgress;
        if (!turnInProgress)
        {
            t = 0f;
            GetTargetPosition();
            playerPos = transform.position;

            CalculateOneTurnDistance();
        }

        if (turnInProgress)
        {
            if (target.transform.parent != null)
            {
                targetPos = target.transform.position;
            }

            Move();
        }
    }

    private void GetTargetPosition()
    {
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            targetPos = cam.ScreenToWorldPoint(Input.mousePosition);

            RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector2.zero);
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                target.transform.parent = hit.transform;
            }
            else
            {
                target.transform.parent = null;
            }

            target.transform.position = targetPos;

            var dist = Vector3.Distance(targetPos, transform.position);
            Debug.Log("Distance to target: " + dist);
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
            transform.position = Vector3.Lerp(playerPos, OneTurnRange, t);
        }
    }

    private void CalculateOneTurnDistance()
    {
        var pathDistance = Vector2.Distance(targetPos, playerPos);
        OneTurnRange = Vector3.Lerp(playerPos, targetPos, playerShip.Speed / pathDistance);
    }

    public string GetObjectData()
    {
        return $"Structure: {hitPoints}/{playerShip.Capacity}\nSpeed: {playerShip.Speed}";
    }

    public void TakeDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            StartCoroutine(DestructionProcess());
        }
    }
    
    private IEnumerator DestructionProcess()
    {
        shipAnimator.SetTrigger(DestroyTrigger);
        while (!shipAnimator.GetCurrentAnimatorStateInfo(0).IsName("ExplosionFinished"))
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}