using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace AI.Patrol
{
    [RequireComponent(typeof(AICharacterControl))]
    public class AIPatrolUnitController : MonoBehaviour
    {
        [Tooltip("The patrol speed")] [Range(0, 1)] public float patrolSpeed = .5f;
        [Tooltip("The chase speed")] [Range(0, 1)] public float chaseSpeed = .75f;
        [Tooltip("The wandering time")] public float wanderingTime = 10.0f;
        [Tooltip("The wandering wait time")] public float waitTime = 1.0f;
        [Tooltip("The maximum wander distance")] public float wanderDistance = 5f;

        private Transform patrolTarget;
        private Transform chaseTarget;
        private Vector3 wanderOrigin;
        private AIPatrolUnitStates state;

        private NavMeshAgent navAgent;
        private AICharacterControl characterControl;
        private PatrolGroup patrolGroup;

        /// <summary>
        /// True when has a valid target
        /// </summary>
        public bool hasChaseTarget
        {
            get
            {
                if (patrolGroup)
                {
                    return chaseTarget && patrolGroup.onView > 0;
                }
                return chaseTarget;
            }
        }

        private void Awake()
        {
            navAgent = GetComponentInChildren<NavMeshAgent>();
            characterControl = GetComponent<AICharacterControl>();
            patrolGroup = GetComponentInParent<PatrolGroup>();

            if (wanderingTime < 0)
            {
                throw new ArgumentOutOfRangeException("wanderingTime", "Wandering time must be positive");
            }
            if (waitTime < 0)
            {
                throw new ArgumentOutOfRangeException("waitTime", "Wait time must be positive");
            }
            if (wanderDistance < 0)
            {
                throw new ArgumentOutOfRangeException("wanderDistance", "Wander distance time must be positive");
            }
        }

        private void Start()
        {
            patrolTarget = characterControl.target;
        }

        private void Update()
        {
            StateUpdate();
        }

        private void OnLoSEnter(Transform target)
        {
            patrolGroup.onView++;
            chaseTarget = target;
        }

        private void OnLoSStay(Transform target)
        {
        }

        private void OnLoSExit(Transform target)
        {
            patrolGroup.onView--;
//            chaseTarget = null;
        }

        /// <summary>
        /// FSM tick
        /// </summary>
        private void StateUpdate()
        {
            // Update the current state and act accordingly
            var oldSate = state;
            if (StateTransitions())
            {
                OnStateExit(oldSate);
                OnStateEnter(state);
            }
            // Execute the state
            OnStateStay(state);
        }

        /// <summary>
        /// FSM state transitions
        /// </summary>
        /// <returns>True when the state changes</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private bool StateTransitions()
        {
            switch (state)
            {
                case AIPatrolUnitStates.Patrol:
                    if (hasChaseTarget)
                    {
                        state = AIPatrolUnitStates.Chasing;
                        return true;
                    }
                    break;
                case AIPatrolUnitStates.Chasing:
                    if (!hasChaseTarget)
                    {
                        state = AIPatrolUnitStates.Lost;
                        return true;
                    }
                    break;
                case AIPatrolUnitStates.Lost:
                    if (hasChaseTarget)
                    {
                        state = AIPatrolUnitStates.Chasing;
                        return true;
                    }
                    else if (navAgent.remainingDistance < navAgent.stoppingDistance)
                    {
                        state = AIPatrolUnitStates.Waiting;
                        StartCoroutine(ChageStateAfterWaitForSeconds(AIPatrolUnitStates.Patrol, wanderingTime));
                        StartCoroutine(ChageStateAfterWaitForSeconds(AIPatrolUnitStates.Wandering, waitTime));
                        StartCoroutine(
                            ChageStateAfterWaitForSeconds(AIPatrolUnitStates.Waiting, wanderingTime - waitTime));
                        return true;
                    }
                    break;
                case AIPatrolUnitStates.Wandering:
                    if (hasChaseTarget)
                    {
                        state = AIPatrolUnitStates.Chasing;
                        return true;
                    }
                    if (navAgent.remainingDistance < navAgent.stoppingDistance)
                    {
                        state = AIPatrolUnitStates.Waiting;
                        StartCoroutine(ChageStateAfterWaitForSeconds(AIPatrolUnitStates.Wandering, waitTime));
                    }
                    break;
                case AIPatrolUnitStates.Waiting:
                    if (hasChaseTarget)
                    {
                        state = AIPatrolUnitStates.Chasing;
                        return true;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return false;
        }

        /// <summary>
        /// Change the state after the specified seconds
        /// </summary>
        /// <param name="nextState">The next state to go to</param>
        /// <param name="waitTime">How much time to wait</param>
        /// <returns></returns>
        private IEnumerator ChageStateAfterWaitForSeconds(AIPatrolUnitStates nextState, float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            OnStateExit(state);
            state = nextState;
            OnStateEnter(state);
        }

        private void OnStateEnter(AIPatrolUnitStates state)
        {
            switch (state)
            {
                case AIPatrolUnitStates.Patrol:
                    navAgent.speed = patrolSpeed;
                    characterControl.target = patrolTarget;
                    break;
                case AIPatrolUnitStates.Chasing:
                    navAgent.speed = chaseSpeed;
                    characterControl.target = chaseTarget;
                    break;
                case AIPatrolUnitStates.Lost:
                    wanderOrigin = characterControl.target.position;
                    characterControl.target = null;
                    break;
                case AIPatrolUnitStates.Wandering:
                    navAgent.SetDestination(wanderOrigin + UnityEngine.Random.insideUnitSphere * wanderDistance);
                    break;
                case AIPatrolUnitStates.Waiting:
                    navAgent.SetDestination(transform.position);
                    characterControl.target = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnStateStay(AIPatrolUnitStates state)
        {
            switch (state)
            {
                case AIPatrolUnitStates.Patrol:
                    break;
                case AIPatrolUnitStates.Chasing:
                    break;
                case AIPatrolUnitStates.Lost:
                    break;
                case AIPatrolUnitStates.Wandering:
                    break;
                case AIPatrolUnitStates.Waiting:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnStateExit(AIPatrolUnitStates state)
        {
            switch (state)
            {
                case AIPatrolUnitStates.Patrol:
                    break;
                case AIPatrolUnitStates.Chasing:
                    break;
                case AIPatrolUnitStates.Lost:
                    break;
                case AIPatrolUnitStates.Wandering:
                    break;
                case AIPatrolUnitStates.Waiting:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public enum AIPatrolUnitStates
    {
        Patrol,
        Chasing,
        Lost,
        Wandering,
        Waiting
    }
}