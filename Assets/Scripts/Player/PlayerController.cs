﻿using System.Collections;
using AstronomicalObject;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Player
{
    public class PlayerController : MonoBehaviour, IObjectData, ITakeDamage
    {
        [SerializeField] private Camera cam;
        [SerializeField] private GameObject target;
        [SerializeField] private GameObject damageTextPrefab;

        public ShipData ShipData { get; set; }

        private int hitPoints;
        private GameManager gameManager;
        private Animator shipAnimator;
        private Vector2 targetPos;
        private Vector2 playerPos;
        private float t;

        private static readonly int DestroyTrigger = Animator.StringToHash("Destroy");

        private void Start()
        {
            ShipData = ShipDataFactory.GetPlayerDefaultShipData();
            hitPoints = ShipData.Capacity;
            targetPos = transform.position;
            shipAnimator = GetComponentInChildren<Animator>();
            gameManager = FindObjectOfType<GameManager>();
            hitPoints = ShipData.Capacity;
        }

        private void Update()
        {
            var turnInProgress = gameManager.TurnInProgress;
            if (!turnInProgress)
            {
                t = 0f;
                GetTargetPosition();
                playerPos = transform.position;
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
            if (gameManager.PlayerIsTargeting)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                var cursorPos = targetPos = cam.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(cursorPos, Vector2.zero);

                if (hit.collider?.GetComponent<SpaceItem>() != null)
                {
                    return;
                }
                
                target.transform.parent = hit.collider?.GetComponent<ICanBeFollowed>() != null ? hit.transform : null;

                targetPos = cursorPos;
                target.transform.position = targetPos;

                var dist = Vector3.Distance(targetPos, transform.position);
                Debug.Log("Distance to target: " + dist);
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

                    t += Time.deltaTime / GameManager.TurnDuration;
                    var oneTurnRange = Helper
                        .CalculateOneTurnDistance(playerPos, targetPos, ShipData.Engine.Speed * 0.01f);
                    transform.position = Vector3.Lerp(playerPos, oneTurnRange, t);
                }
            }

            public string GetObjectData()
            {
                return $"Structure: {hitPoints}/{ShipData.Capacity}\nSpeed: {ShipData.Engine.Speed}";
            }

            public void TakeDamage(int damage)
            {
                var objectPosition = transform.position;
                var textPosition = new Vector3(objectPosition.x, objectPosition.y + 0.3f, objectPosition.z);
                var text = Instantiate(damageTextPrefab, textPosition, Quaternion.identity, transform);
                text.GetComponent<TextMesh>().text = damage.ToString();

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
    }