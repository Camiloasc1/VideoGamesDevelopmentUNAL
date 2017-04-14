using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [Tooltip("The initial velocity")] public float velocity = 1.0f;
        [Tooltip("The initial life span")] public float lifeSpan = 5.0f;

        private void Start()
        {
            GetComponent<Rigidbody>().velocity = transform.forward * velocity;
            Destroy(gameObject, lifeSpan);
        }
    }
}