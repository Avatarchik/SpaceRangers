using System.Collections;
using UnityEngine;

namespace AstronomicalObject
{
    public class Planet : MonoBehaviour, IObjectData, ICanBeFollowed
    {
        [SerializeField] private int rotationDuration;
        private GameManager gameManager;
        private GameObject star;
        private float turnDuration;
        private Coroutine planetMovementCoroutine;

        private void Start()
        {
            star = GameObject.Find("Star");
            gameManager = FindObjectOfType<GameManager>();
            turnDuration = GameManager.TurnDuration;
        }

        private void Update()
        {
            if (gameManager.TurnInProgress)
            {
                if (planetMovementCoroutine == null)
                {
                    planetMovementCoroutine = StartCoroutine(PlanetMovementCoroutine());
                }
            }
        }

        private IEnumerator PlanetMovementCoroutine()
        {
            var timer = 0f;
            while (timer < turnDuration)
            {
                var speed = -360f / turnDuration / rotationDuration;
                transform.RotateAround(star.transform.position, star.transform.forward, speed * Time.deltaTime);
                transform.localRotation = Quaternion.identity;
                timer += Time.deltaTime;
                yield return null;
            }

            planetMovementCoroutine = null;
        }

        public string GetObjectData()
        {
            return $"Rotation period: {rotationDuration} Earth days";
        }
    }
}