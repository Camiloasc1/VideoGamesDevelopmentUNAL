using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class LoS : MonoBehaviour
    {
        [Tooltip("The field of view in degrees")] [Range(0, 360)] public float fieldOfView = 30f;
        [Tooltip("The eyes offset")] public Vector3 eyesOffset = new Vector3(0, 1.42f, 0);
        [Tooltip("The maximum view distance")] public float viewDistance = 10;
        [Tooltip("The maximum hear distance")] public float hearDistance = 2.5f;
        [Tooltip("The list of GOs to look for")] public List<GameObject> targets;

        private readonly Dictionary<GameObject, bool> targetWasOnView = new Dictionary<GameObject, bool>();

        private void Reset()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                targets.Add(player);
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, hearDistance);
            Gizmos.DrawWireSphere(transform.position, viewDistance);
            Gizmos.DrawLine(transform.position,
                transform.position + Quaternion.Euler(0, fieldOfView, 0) * transform.forward * viewDistance);
            Gizmos.DrawLine(transform.position,
                transform.position + Quaternion.Euler(0, -fieldOfView, 0) * transform.forward * viewDistance);
            Gizmos.DrawLine(transform.position,
                transform.position + Quaternion.Euler(fieldOfView, 0, 0) * transform.forward * viewDistance);
            Gizmos.DrawLine(transform.position,
                transform.position + Quaternion.Euler(-fieldOfView, 0, 0) * transform.forward * viewDistance);

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

        private bool CanSeeTarget(GameObject target)
        {
            var toTarget = target.transform.position - transform.position;
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

        private bool RaycastToTarget(Vector3 toTarget, GameObject target)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position + eyesOffset, toTarget, out hitInfo, toTarget.magnitude))
            {
                return hitInfo.transform.gameObject == target;
            }
            return false;
        }
    }
}