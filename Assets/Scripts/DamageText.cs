using UnityEngine;

public class DamageText : MonoBehaviour
{
    private float lifeTime = 1.5f;
    private void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * (Time.deltaTime * 0.5f), Space.World);
        transform.rotation = Quaternion.identity;
    }
}
