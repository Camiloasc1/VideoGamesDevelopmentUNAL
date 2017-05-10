using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(ThirdPersonCharacter))]
    public class AICharacterControl : MonoBehaviour
    {
        [Tooltip("The target transform to aim for")] public Transform target; // target to aim for
        [Tooltip("Move to target on Update()")] public bool moveOnUpdate;
        [Tooltip("Initialize the offset at Start()")] public bool useStartOffset;
        [Tooltip("The current offset")] public Vector3 offset;
        [Tooltip("When to use the relative position")] public bool useRelativePosition;
        [Tooltip("When to use the relative rotation")] public bool useRelativeRotation;

        private NavMeshAgent agent; // the navmesh agent required for the path finding
        private ThirdPersonCharacter character; // the character we are controlling

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
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
        }

        private void Start()
        {
            if (useStartOffset)
            {
                offset = target.InverseTransformDirection(transform.position - target.position);
            }

            agent.updateRotation = false;
            agent.updatePosition = true;
        }

        private void Update()
        {
            if (moveOnUpdate)
            {
                MoveToTarget();
            }

            character.Move(agent.remainingDistance > agent.stoppingDistance ? agent.desiredVelocity : Vector3.zero,
                false, false);
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