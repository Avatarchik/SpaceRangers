using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AstronomicalObject
{
    public class Asteroid : MonoBehaviour, IObjectData, ITakeDamage
    {
        [SerializeField] private List<GameObject> mineralPrefabs;
        private Animator animator;
        private int mineralsAmount;
        private static readonly int DestroyTrigger = Animator.StringToHash("Destroy");

        private void Start()
        {
            mineralsAmount = Random.Range(50, 150);
            animator = gameObject.transform.Find("Graphics").GetComponent<Animator>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            var takeDamage = other.GetComponent<ITakeDamage>();
            takeDamage?.TakeDamage(mineralsAmount);
            StartCoroutine(DestructionProcess());
        }

        private IEnumerator DestructionProcess()
        {
            animator.SetTrigger(DestroyTrigger);
            var mineralObjectsAmount = Random.Range(2, 4);
            for (var i=0; i<mineralObjectsAmount; i++)
            {
                var randomMineralPrefab = mineralPrefabs[Random.Range(0, mineralPrefabs.Count)];
                var mineralsObject =Instantiate(randomMineralPrefab, GetRandomPositionForMineral(), Quaternion.identity);
                mineralsObject.name = "Minerals";
                mineralsObject.GetComponent<Mineral>().Amount = mineralsAmount / mineralObjectsAmount;
            }
            while (!animator.GetCurrentAnimatorStateInfo(0).IsName("ExplosionFinished"))
            {
                yield return null;
            }
            Destroy(gameObject);
        }

        private Vector3 GetRandomPositionForMineral()
        {
            var position = transform.position;
            Random.Range(position.x - 0.5f, position.x + 0.5f);
            return new Vector3(Random.Range(position.x - 0.5f, position.x + 0.5f),
                Random.Range(position.y - 0.5f, position.y + 0.5f), 0f);
        }

        public string GetObjectData()
        {
            return $"Contains up to {mineralsAmount} minerals";
        }

        public void TakeDamage(int damage)
        {
            StartCoroutine(DestructionProcess());
        }
    }
}
