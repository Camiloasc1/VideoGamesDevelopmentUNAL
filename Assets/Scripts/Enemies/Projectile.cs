using Player;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [Tooltip("The initial speed")] public float speed = 100f;
        [Tooltip("The initial life span")] public float lifeSpan = 1f;
        [Tooltip("The damaga caused by this projectile")] public float damage = 1f;

        [HideInInspector] public Weapon weapon;
        [HideInInspector] public GameObject instigator;

        private Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        private void Start()
        {
            rb.velocity = transform.forward * speed;
            Destroy(gameObject, lifeSpan);
        }

        private void FixedUpdate()
        {
            // Check impacts for high speed projectiles
            var sweepHits = rb.SweepTestAll(transform.forward, speed * Time.fixedDeltaTime);
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
                // Damage to player
                other.GetComponent<PlayerCharacter>().Damage(damage);
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