using System.Collections;
using Equipment;
using Player;
using UnityEngine;

namespace AstronomicalObject
{
    public abstract class SpaceItem : MonoBehaviour, ITakeDamage
    {
        private Animator animator;
        private GameObject player;
        private static readonly int DestroyTrigger = Animator.StringToHash("Destroy");
        private GameManager gameManager;
        private LineRenderer playerGrabRangeCircle;
        
        public Item Content { get; set; }

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
            player = GameObject.Find("Player");
            gameManager = FindObjectOfType<GameManager>();

        }
        
        public void TakeDamage(int damage)
        {
            StartCoroutine(DestroyProcess());
        }

        private IEnumerator DestroyProcess()
        {
            animator.SetTrigger(DestroyTrigger);
            while (!animator.GetCurrentAnimatorStateInfo(0).IsName("ExplosionFinished"))
            {
                yield return null;
            }
            Destroy(gameObject);
        }

        private void OnMouseEnter()
        {
            if (gameManager.TurnInProgress || gameManager.PlayerIsTargeting)
            {
                return;
            }

            CircleDrawer.DrawCircle(player, 
                    player.GetComponent<PlayerController>().ShipData.Grab.Range * 0.01f, Color.yellow);
                playerGrabRangeCircle = player.GetComponent<LineRenderer>();
        }

        private void OnMouseExit()
        {
            Destroy(playerGrabRangeCircle);
        }
    }
}