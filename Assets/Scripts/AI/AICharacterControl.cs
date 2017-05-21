using UnityEngine;
using UnityEngine.AI;

namespace AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class AICharacterControl : MonoBehaviour
    {
        [Tooltip("The target transform to aim for")] public Transform target;
        [Tooltip("Move to target on Update()")] public bool moveOnUpdate;
        [Tooltip("Initialize the offset at Start()")] public bool useStartOffset;
        [Tooltip("The current offset")] public Vector3 offset;
        [Tooltip("When to use the relative position")] public bool useRelativePosition;
        [Tooltip("When to use the relative rotation")] public bool useRelativeRotation;

        private NavMeshAgent agent;

        public Vector3 Destination
        {
            get
            {
                return target.position + (useRelativePosition
                           ? (useRelativeRotation ? target.TransformDirection(offset) : offset)
                           : Vector3.zero);
            }
        }

        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            if (useStartOffset)
            {
                offset = target.InverseTransformDirection(transform.position - target.position);
            }
        }

        private void Update()
        {
            if (moveOnUpdate)
            {
                MoveToTarget();
            }
        }

        public void MoveToTarget()
        {
            if (target != null)
            {
                agent.SetDestination(Destination);
            }
        }
    }
}