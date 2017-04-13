using System;
using System.Collections.Generic;
using UnityEngine;

namespace AI
{
    public class LoS : MonoBehaviour
    {
        [Tooltip("The field of view in degrees")] [Range(0, 360)] public float FoV = 30f;
        [Tooltip("The eyes offset")] public Vector3 EyesOffset = new Vector3(0, 1.42f, 0);
        [Tooltip("The maximum view distance")] public float ViewDistance = 10;
        [Tooltip("The maximum hear distance")] public float HearDistance = 2.5f;
        [Tooltip("The list of GOs to look for")] public List<GameObject> LookFor;

        private void Reset()
        {
            var player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                LookFor.Add(player);
            }
        }

        private void Awake()
        {
        }

        private void Start()
        {
        }

        private void Update()
        {
            foreach (var target in LookFor)
            {
                CanSeeTarget(target);
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, HearDistance);
            Gizmos.DrawWireSphere(transform.position, ViewDistance);
            Gizmos.DrawLine(transform.position,
                transform.position + Quaternion.Euler(0, FoV, 0) * transform.forward * ViewDistance);
            Gizmos.DrawLine(transform.position,
                transform.position + Quaternion.Euler(0, -FoV, 0) * transform.forward * ViewDistance);

            Gizmos.color = Color.green;
            if (Application.isPlaying)
            {
                foreach (var target in LookFor)
                {
                    if (CanSeeTarget(target))
                    {
                        Gizmos.DrawLine(transform.position + EyesOffset, target.transform.position + EyesOffset);
                    }
                }
            }
        }

        private bool CanSeeTarget(GameObject target)
        {
            var toTarget = target.transform.position - transform.position;
            var toTargetMagnitude = toTarget.magnitude; // Avoid the property's internal sqrt each time

            // Is near enough
            if (toTargetMagnitude < HearDistance)
            {
                // Has a clear view of the target
                return RaycastToTarget(toTarget, target);
            }

            // Is far enough
            if (toTargetMagnitude > ViewDistance)
            {
                return false;
            }

            // Is inside the FoV
            if (Vector3.Angle(transform.forward, toTarget) > FoV)
            {
                return false;
            }

            // Has a clear view of the target
            return RaycastToTarget(toTarget, target);
        }

        private bool RaycastToTarget(Vector3 toTarget, GameObject target)
        {
            RaycastHit hitInfo;
            if (Physics.Raycast(transform.position + EyesOffset, toTarget, out hitInfo, toTarget.magnitude))
            {
                return hitInfo.transform.gameObject == target;
            }
            return false;
        }
    }
}