using UnityEngine;

namespace Enemies
{
    public class ShooterWeapon : MonoBehaviour
    {
        [Tooltip("The projectile prefab")] public GameObject projectileTemplate;
        [Tooltip("The weapon offset")] public Vector3 weaponOffset = new Vector3(0, 1.0f, 0);
        [Tooltip("The shoot cone size in degrees")] [Range(0, 180)] public float shootAngle = 10f;

        private void Update()
        {
            if (Time.frameCount % 10 == 0)
            {
                SpawnProjectile();
            }
        }

        private void OnDrawGizmosSelected()
        {
            var projectile = projectileTemplate.GetComponent<Projectile>();
            if (!projectile)
            {
                return;
            }
            var viewDistance = projectile.lifeSpan / projectile.velocity;

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position + weaponOffset,
                transform.position + weaponOffset + transform.rotation * Quaternion.Euler(0, shootAngle, 0) *
                Vector3.forward * viewDistance);
            Gizmos.DrawLine(transform.position + weaponOffset,
                transform.position + weaponOffset + transform.rotation * Quaternion.Euler(0, -shootAngle, 0) *
                Vector3.forward * viewDistance);
        }

        public void SpawnProjectile()
        {
            var position = transform.position + weaponOffset;
            var rotation = transform.rotation * Quaternion.Euler(0, Random.Range(-shootAngle, shootAngle), 0);
            Instantiate(projectileTemplate, position, rotation, transform.parent);
        }
    }
}