using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class LoS : MonoBehaviour
    {
        [Tooltip("The field of view in degrees")] [Range(0, 180)] public float fieldOfView = 30f;
        [Tooltip("The eyes offset")] public Vector3 eyesOffset = new Vector3(0, 1.42f, 0);
        [Tooltip("The maximum view distance")] public float viewDistance = 10f;
        [Tooltip("The maximum hear distance")] public float hearDistance = 2.5f;
        [Tooltip("The list of targets to look for")] public List<Transform> targets;

        private readonly Dictionary<Transform, bool> targetWasOnView = new Dictionary<Transform, bool>();

        private void Reset()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                targets.Add(player.transform);
            }
        }

        private void Start()
        {
            var lantern = GetComponentInChildren<Light>();
            if (lantern)
            {
                lantern.type = LightType.Spot;
                lantern.spotAngle = fieldOfView * 2;
                lantern.range = viewDistance;
            }
        }

        private void OnEnable()
        {
            foreach (var target in targets)
            {
                targetWasOnView[target] = false;
            }
        }

        private void Update()
        {
            foreach (var target in targets)
            {
                var canSee = CanSeeTarget(target);
                if (!targetWasOnView[target] && canSee)
                {
                    SendMessage("OnLoSEnter", target, SendMessageOptions.DontRequireReceiver);
                }
                else if (targetWasOnView[target] && canSee)
                {
                    SendMessage("OnLoSStay", target, SendMessageOptions.DontRequireReceiver);
                }
                else if (targetWasOnView[target] && !canSee)
                {
                    SendMessage("OnLoSExit", target, SendMessageOptions.DontRequireReceiver);
                }
                targetWasOnView[target] = canSee;
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, hearDistance);
            Gizmos.DrawWireSphere(transform.position, viewDistance);
            Gizmos.DrawLine(transform.position + eyesOffset,
                transform.position + eyesOffset + transform.rotation * Quaternion.Euler(0, fieldOfView, 0) *
                Vector3.forward * viewDistance);
            Gizmos.DrawLine(transform.position + eyesOffset,
                transform.position + eyesOffset + transform.rotation * Quaternion.Euler(0, -fieldOfView, 0) *
                Vector3.forward * viewDistance);
            Gizmos.DrawLine(transform.position + eyesOffset,
                transform.position + eyesOffset + transform.rotation * Quaternion.Euler(fieldOfView, 0, 0) *
                Vector3.forward * viewDistance);
            Gizmos.DrawLine(transform.position + eyesOffset,
                transform.position + eyesOffset + transform.rotation * Quaternion.Euler(-fieldOfView, 0, 0) *
                Vector3.forward * viewDistance);

            Gizmos.color = Color.green;
            if (Application.isPlaying)
            {
                foreach (var target in targets)
                {
                    if (CanSeeTarget(target))
                    {
                        Gizmos.DrawLine(transform.position + eyesOffset, target.transform.position + eyesOffset);
                    }
                }
            }
        }

        /// <summary>
        /// Detect wether the target is visible (inside the field of view and a clear view to it)
        /// </summary>
        /// <param name="target">The target's transform</param>
        /// <returns>True if visible, False otherwise</returns>
        private bool CanSeeTarget(Transform target)
        {
            var toTarget = target.position - transform.position;
            var toTargetMagnitude = toTarget.magnitude; // Avoid the property's internal sqrt each time

            // Is near enough
            if (toTargetMagnitude < hearDistance)
            {
                // Has a clear view of the target
                return RaycastToTarget(toTarget, target);
            }

            // Is far enough
            if (toTargetMagnitude > viewDistance)
            {
                return false;
            }

            // Is inside the FoV
            if (Vector3.Angle(transform.forward, toTarget) > fieldOfView)
            {
                return false;
            }

            // Has a clear view of the target
            return RaycastToTarget(toTarget, target);
        }

        /// <summary>
        /// Chech if has a clear view of the target
        /// </summary>
        /// <param name="toTarget">Relative vector to the target</param>
        /// <param name="target">The target's transform</param>
        /// <returns>True if visible, False otherwise</returns>
        private bool RaycastToTarget(Vector3 toTarget, Transform target)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position + eyesOffset, toTarget, out hitInfo, toTarget.magnitude))
            {
                return hitInfo.transform == target;
            }
            return false;
        }
    }
}