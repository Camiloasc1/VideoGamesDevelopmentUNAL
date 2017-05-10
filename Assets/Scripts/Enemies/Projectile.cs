using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [Tooltip("The initial speed")] public float speed = 100.0f;
        [Tooltip("The initial life span")] public float lifeSpan = 1.0f;

        [HideInInspector] public Weapon weapon;
        [HideInInspector] public GameObject instigator;

        private Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            rigidbody.velocity = transform.forward * speed;
            Destroy(gameObject, lifeSpan);
        }

        private void FixedUpdate()
        {
            // Check impacts for high speed projectiles
            var sweepHits = rigidbody.SweepTestAll(transform.forward, speed * Time.fixedDeltaTime);
            foreach (var hit in sweepHits)
            {
                OnTriggerEnter(hit.collider);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            CheckImpact(other.gameObject);
        }

        private void CheckImpact(GameObject other)
        {
            if (other.CompareTag("Player"))
            {
                print("Damage " + Time.timeSinceLevelLoad);
                // Damage to player
                Destroy(gameObject);
            }
            else if (other.CompareTag("Projectile") || other.CompareTag("Enemy"))
            {
                // Ignore the enemies or other projectiles
            }
            else
            {
                // Hit the world
                Destroy(gameObject);
            }
        }
    }
}