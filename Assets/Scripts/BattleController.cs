using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.UNetWeaver;
using UnityEngine;
using UnityEngine.EventSystems;

public class BattleController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private LineRenderer laserBeam;
    private GameManager gameManager;
    private List<GameObject> weaponRangeZone = new List<GameObject>();
    private GameObject target;
    private GameObject targetMark;
    private int lastAttackTurnId;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        laserBeam.startWidth = 0.02f;
        laserBeam.sortingOrder = 15;
        laserBeam.enabled = false;
    }

    private void Update()
    {
        if (gameManager.TurnInProgress && target != null && lastAttackTurnId < gameManager.TurnId)
        {
            StartCoroutine(AttackProcess());
            lastAttackTurnId = gameManager.TurnId;
        }
        
        if (!gameManager.PlayerIsTargeting)
        {
            return;
        }
        
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            var targetPos = cam.ScreenToWorldPoint(Input.mousePosition);
            var hit = Physics2D.Raycast(targetPos, Vector2.zero);
            var hitCollider = hit.collider;
            //TODO: don't select yourself as target
            if (hitCollider == null || hitCollider.GetComponent<ITakeDamage>() == null)
            {
                return;
            }

            if (hitCollider.gameObject == target)
            {
                return;
            }

            var maxWeaponRange = transform.GetComponent<Ship>().Weapons.Max(it => it.Range) * 0.01f;
            var distanceToTarget = Vector3.Distance(transform.position,
                hitCollider.gameObject.transform.position);

            if (distanceToTarget < maxWeaponRange)
            {
                Destroy(targetMark);
                targetMark = null;

                target = hitCollider.gameObject;
                targetMark = Instantiate(targetPrefab, target.transform);
                var targetMarkPosition = targetMark.transform.position;
                targetMark.transform.position = new Vector3(targetMarkPosition.x + 0.2f,
                    targetMarkPosition.y + 0.2f, 0);
                Debug.Log($"Selected target: {target}");
            }
        }
    }

    public void StartStopTargeting()
    {
        if (gameManager.TurnInProgress)
        {
            return;
        }

        if (gameManager.PlayerIsTargeting)
        {
            foreach (var weaponRangeCircle in weaponRangeZone)
            {
                Destroy(weaponRangeCircle);
            }

            weaponRangeZone.Clear();
            gameManager.PlayerIsTargeting = false;
            return;
        }

        gameManager.PlayerIsTargeting = true;
        var weapons = transform.GetComponent<Ship>().Weapons;
        foreach (var weapon in weapons)
        {
            var obj = new GameObject("Weapon Range Zone");
            obj.transform.parent = transform;
            var position = transform.position;
            obj.transform.position = new Vector3(position.x, position.y, -1f);
            CircleDrawer.DrawCircle(obj, weapon.Range * 0.01f, 0.01f);
            weaponRangeZone.Add(obj);
        }
    }

    private IEnumerator AttackProcess()
    {
        laserBeam.enabled = true;
        var timer = Time.time + 1f;
        while (Time.time < timer)
        {
            laserBeam.SetPosition(0, transform.position);
            laserBeam.SetPosition(1, target.transform.position);
            yield return null;
        }

        laserBeam.enabled = false;
        foreach (var weapon in transform.GetComponent<Ship>().Weapons)
        {
            target.GetComponent<ITakeDamage>().TakeDamage(weapon.Damage);
        }
    }
}