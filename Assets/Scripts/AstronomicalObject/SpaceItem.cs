using System.Collections;
using UnityEngine;

namespace AstronomicalObject
{
    public abstract class SpaceItem : MonoBehaviour, ITakeDamage
    {
        private Animator animator;
        private static readonly int DestroyTrigger = Animator.StringToHash("Destroy");

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
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
    }
}