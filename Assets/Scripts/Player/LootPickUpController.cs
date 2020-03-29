using System.Collections.Generic;
using AstronomicalObject;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Player
{
    public class LootPickUpController : MonoBehaviour
    {
        [SerializeField] private Camera cam;
        [SerializeField] private GameObject pickUpMarkPrefab;
        [SerializeField] private Text bottomPanelFreeSpaceText;

        private List<GameObject> lootToPickUp = new List<GameObject>();
        private GameManager gameManager;

        private void Start()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        private void Update()
        {
            if (!gameManager.TurnInProgress && !gameManager.PlayerIsTargeting
                                            && Input.GetMouseButtonDown(0) &&
                                            !EventSystem.current.IsPointerOverGameObject())
            {
                var targetPos = cam.ScreenToWorldPoint(Input.mousePosition);

                RaycastHit2D hit = Physics2D.Raycast(targetPos, Vector2.zero);

                if (hit.collider != null && hit.collider.GetComponent<SpaceItem>())
                {
                    var item = hit.collider.gameObject;
                    if (lootToPickUp.Contains(item))
                    {
                        Destroy(item.GetComponentInChildren<PickUpMark>().gameObject);
                        lootToPickUp.Remove(item);
                        return;
                    }

                    var pickUpMark = Instantiate(pickUpMarkPrefab, hit.collider.transform, true);
                    var pickUpMarkPosition = hit.collider.transform.position;
                    pickUpMark.transform.position
                        = new Vector3(pickUpMarkPosition.x + 0.12f, pickUpMarkPosition.y + 0.12f, pickUpMarkPosition.z);
                    lootToPickUp.Add(item);
                    Debug.Log($"Items to pick up: {lootToPickUp.Count}");
                }
            }

            if (gameManager.TurnInProgress)
            {
                for (var i = 0; i < lootToPickUp.Count; i++)
                {
                    var spaceItem = lootToPickUp[i];
                    if (Vector3.Distance(transform.position, spaceItem.transform.position)
                        < transform.GetComponent<PlayerController>().ShipData.Grab.Range * 0.01f)
                    {
                        Vector3 moveDir = (transform.position - spaceItem.transform.position).normalized;
                        spaceItem.transform.position += moveDir * (2f * Time.deltaTime);
                    }

                    if (Vector3.Distance(spaceItem.transform.position, transform.position) < 0.05)
                    {
                        var shipData = GetComponent<PlayerController>().ShipData;
                        shipData.Cargo.Add(spaceItem.GetComponent<SpaceItem>().Content);
                        bottomPanelFreeSpaceText.text = shipData.GetFreeSpace().ToString();
                        lootToPickUp.Remove(spaceItem);
                        i--;
                        Destroy(spaceItem.gameObject);
                    }
                }
            }
        }
    }
}