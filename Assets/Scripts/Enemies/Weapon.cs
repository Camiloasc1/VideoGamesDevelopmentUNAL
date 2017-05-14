using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    [RequireComponent(typeof(Light))]
    [RequireComponent(typeof(ParticleSystem))]
    public class Weapon : MonoBehaviour
    {
        [Tooltip("The projectile prefab")] public Projectile projectileTemplate;
        [Tooltip("The projectile pool")] public Transform projectilePool;
        [Tooltip("The shoot cone size in degrees")] [Range(0, 180)] public float shootAngle = 10f;
        [Tooltip("The delay between shoots")] public float fireRate = 0.25f;
        [Tooltip("The reload time")] public float reloadTime = 2.5f;
        [Tooltip("The ammount of ammo per clip")] public int ammoPerClip = 10;
        [Header("Status")] [Tooltip("The weapon shooting")] public bool isShooting;
        public Color normalColor;
        public Color warningColor;
        public Color dangerColor;

        private ParticleSystem gunFlare;
        private Light lantern;

        private int clipAmmo;
        private bool canShoot;

        public float ViewDistance
        {
            get { return projectileTemplate.lifeSpan * projectileTemplate.speed; }
        }

        private void Awake()
        {
            gunFlare = GetComponent<ParticleSystem>();
            lantern = GetComponent<Light>();

            if (!projectileTemplate)
            {
                throw new ArgumentNullException("projectileTemplate", "ProjectileTemplate can not be null");
            }
            if (ammoPerClip < 1)
            {
                throw new ArgumentOutOfRangeException("ammoPerClip", "AmmoPerClip must be > 1");
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

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position,
                transform.position + transform.rotation * Quaternion.Euler(0, shootAngle, 0) * Vector3.forward *
                ViewDistance);
            Gizmos.DrawLine(transform.position,
                transform.position + transform.rotation * Quaternion.Euler(0, -shootAngle, 0) * Vector3.forward *
                ViewDistance);
        }

        /// <summary>
        /// Try to shoot to the target
        /// </summary>
        /// <param name="target">Target position</param>
        /// <returns>True if projectile spawned, False if reloading or in inter-shoot delay or target not reachable</returns>
        public bool TryShoot(Vector3 target)
        {
            var toTarget = target - transform.position;
            transform.localEulerAngles = new Vector3(Quaternion.LookRotation(toTarget).eulerAngles.x, 0, 0);

            // Is far enough
            if (toTarget.magnitude > ViewDistance)
            {
                return false;
            }
            // Is inside the FoV
            if (Vector3.Angle(transform.forward, toTarget) > shootAngle)
            {
                return false;
            }
            return TryShoot();
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

        /// <summary>
        /// Spawn the projectile
        /// </summary>
        private void Shoot()
        {
            // Spawn projectile
            var rotation = transform.rotation * Quaternion.Euler(0, Random.Range(-shootAngle, shootAngle), 0);
            var projectile = Instantiate(projectileTemplate, transform.position, rotation, projectilePool)
                .GetComponent<Projectile>();
            projectile.weapon = this;
            projectile.instigator = transform.parent.gameObject;

            clipAmmo--;
            gunFlare.Emit(1);
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

        public void SetLanternState(LanternStates state)
        {
            switch (state)
            {
                case LanternStates.Normal:
                    lantern.color = normalColor;
                    break;
                case LanternStates.Warning:
                    lantern.color = warningColor;
                    break;
                case LanternStates.Danger:
                    lantern.color = dangerColor;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("state", state, null);
            }
        }
    }

    public enum LanternStates
    {
        Normal,
        Warning,
        Danger
    }
}