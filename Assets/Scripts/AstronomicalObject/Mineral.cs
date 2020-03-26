using System;
using System.Collections;
using UnityEngine;

namespace AstronomicalObject
{
    public class Mineral : MonoBehaviour, IObjectData, ITakeDamage
    {
        private Animator animator;
        private static readonly int DestroyTrigger = Animator.StringToHash("Destroy");
        public int Amount { get; set; }
        public int Weight { get; private set; }

        private void Start()
        {
            animator = GetComponentInChildren<Animator>();
        }

        public string GetObjectData()
        {
            return $"Amount: {Amount}";
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