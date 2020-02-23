using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Npc : MonoBehaviour
{
    private GameManager gameManager;
    private GameObject star;
    [SerializeField] private GameObject target;

    private void Start()
    {
        star = GameObject.Find("Star");
        gameManager = FindObjectOfType<GameManager>();
        target.transform.parent = null;
        FindTargetToMove();
    }

    void Update()
    {
        bool turnInProgress = gameManager.TurnInProgress;
        if (!turnInProgress && transform.position == target.transform.position)
        {
            FindTargetToMove();
        }

        if (turnInProgress)
        {
            Move(target.transform.position);
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

            transform.position = Vector2.MoveTowards(transform.position, targetPos, 0.8f * Time.deltaTime);
        }
    }

    private void FindTargetToMove()
    {
        var starPosition = star.transform.position;
        var targetX = Random.Range(starPosition.x - 3f, starPosition.y + 5f);
        var targetY = Random.Range(starPosition.x - 3f, starPosition.y + 5f);
        target.transform.position = new Vector3(targetX, targetY, 0f);
    }
}