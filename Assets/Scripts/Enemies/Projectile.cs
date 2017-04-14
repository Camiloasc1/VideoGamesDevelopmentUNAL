using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [Tooltip("The initial velocity")] public float velocity = 10.0f;
        [Tooltip("The initial life span")] public float lifeSpan = 1.0f;

        [HideInInspector] public ShooterWeapon instigator;

        private void Start()
        {
            GetComponent<Rigidbody>().velocity = transform.forward * velocity;
            Destroy(gameObject, lifeSpan);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                // Damage to player
                Destroy(gameObject);
            }
            else if (other.gameObject == instigator.gameObject)
            {
                // Ignore the instigator
            }
            else
            {
                // Hit the world
                Destroy(gameObject);
            }
        }
    }
}