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

        private Vector3 offset;

        private NavMeshAgent agent; // the navmesh agent required for the path finding
        private ThirdPersonCharacter character; // the character we are controlling

        private void Awake()
        {
            // get the components on the object we need ( should not be null due to require component so no need to check )
            agent = GetComponentInChildren<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();
        }

        private void Start()
        {
            offset = transform.position - target.position;

            agent.updateRotation = false;
            agent.updatePosition = true;
        }

        private void Update()
        {
            if (target != null)
            {
                agent.SetDestination(target.position + offset);
            }

            character.Move(agent.remainingDistance > agent.stoppingDistance ? agent.desiredVelocity : Vector3.zero,
                false, false);
        }
    }
}