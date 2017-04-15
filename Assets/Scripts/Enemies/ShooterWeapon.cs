using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class ShooterWeapon : MonoBehaviour
    {
        [Tooltip("The projectile prefab")] public GameObject projectileTemplate;
        [Tooltip("The weapon offset")] public Vector3 weaponOffset = new Vector3(0, 1.0f, 0);
        [Tooltip("The shoot cone size in degrees")] [Range(0, 180)] public float shootAngle = 10f;
        [Tooltip("The delay between shoots")] public float fireRate = 0.25f;
        [Tooltip("The reload time")] public float reloadTime = 2.5f;
        [Tooltip("The ammount of ammo per clip")] public int ammoPerClip = 10;
        [Header("Status")] [Tooltip("The weapon shooting")] public bool isShooting;

        private int clipAmmo;
        private bool canShoot;

        private void Awake()
        {
            if (!projectileTemplate)
            {
                throw new ArgumentNullException("projectileTemplate", "Projectile Template can not be null");
            }
            if (ammoPerClip < 1)
            {
                throw new ArgumentOutOfRangeException("ammoPerClip", "Ammo Per Clip time must be > 1");
            }
        }

        private void Update()
        {
            if (isShooting)
            {
                TryShoot();
            }
        }

        private void OnDrawGizmosSelected()
        {
            if (!projectileTemplate)
            {
                return;
            }
            var projectile = projectileTemplate.GetComponent<Projectile>();
            if (!projectile)
            {
                return;
            }
            var viewDistance = projectile.lifeSpan * projectile.velocity;

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position + weaponOffset,
                transform.position + weaponOffset + transform.rotation * Quaternion.Euler(0, shootAngle, 0) *
                Vector3.forward * viewDistance);
            Gizmos.DrawLine(transform.position + weaponOffset,
                transform.position + weaponOffset + transform.rotation * Quaternion.Euler(0, -shootAngle, 0) *
                Vector3.forward * viewDistance);
        }

        /// <summary>
        /// Try to shoot
        /// </summary>
        /// <returns>True if projectile spawned, False if reloading or in inter-shoot delay</returns>
        public bool TryShoot()
        {
            if (clipAmmo == 0)
            {
                StartCoroutine(CanShootDelay());
                return false;
            }
            if (!canShoot)
            {
                return false;
            }
            Shoot();
            return true;
        }

        private void Shoot()
        {
            // Spawn projectile
            var position = transform.position + weaponOffset;
            var rotation = transform.rotation * Quaternion.Euler(0, Random.Range(-shootAngle, shootAngle), 0);
            var projectile = Instantiate(projectileTemplate, position, rotation, transform.parent)
                .GetComponent<Projectile>();
            projectile.instigator = this;

            clipAmmo--;
            StartCoroutine(CanShootDelay());
        }

        private IEnumerator CanShootDelay()
        {
            canShoot = false;
            if (clipAmmo == 0)
            {
                clipAmmo = ammoPerClip;
                yield return new WaitForSeconds(reloadTime);
            }
            else
            {
                yield return new WaitForSeconds(fireRate);
            }
            canShoot = true;
        }
    }
}