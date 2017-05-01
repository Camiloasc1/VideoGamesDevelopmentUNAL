using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace AI.CheckPoint
{
    public class AICheckPoint : MonoBehaviour
    {
        [Tooltip("The rotation per step")] public float stepRotation = 90f;
        [Tooltip("The wait time per step")] public float stepWaitTime = 1f;
        [Tooltip("The angular speed")] public float patrolAngularSpeed = 15f;
        [Tooltip("The angular speed")] public float chaseAngularSpeed = 60f;

        private Transform chaseTarget;
        private Quaternion patrolTarget;
        private AIPatrolUnitStates state;

        private Weapon weapon;

        /// <summary>
        /// True when has a valid target
        /// </summary>
        public bool hasChaseTarget
        {
            get { return chaseTarget; }
        }

        private void Awake()
        {
            weapon = GetComponentInChildren<Weapon>();
            if (!weapon)
            {
                throw new ArgumentNullException("weapon", "Weapon not found in children");
            }

            if (stepWaitTime < 0)
            {
                throw new ArgumentOutOfRangeException("stepWaitTime", "StepWaitTime must be positive");
            }
        }

        private void Start()
        {
            patrolTarget = transform.rotation;
        }

        private void Update()
        {
            StateUpdate();
        }

        private void OnLoSEnter(Transform target)
        {
            chaseTarget = target;
        }

        private void OnLoSStay(Transform target)
        {
        }

        private void OnLoSExit(Transform target)
        {
            chaseTarget = null;
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
                    // Step rotation reached
                    if (Quaternion.Angle(transform.rotation, patrolTarget) < 1f)
                    {
                        state = AIPatrolUnitStates.Waiting;
                        StartCoroutine(ChageStateAfterWaitForSeconds(AIPatrolUnitStates.Patrol, stepWaitTime));
                        return true;
                    }
                    break;
                case AIPatrolUnitStates.Chasing:
                    if (!hasChaseTarget)
                    {
                        state = AIPatrolUnitStates.Waiting;
                        StartCoroutine(ChageStateAfterWaitForSeconds(AIPatrolUnitStates.Patrol, stepWaitTime));
                        return true;
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
                    patrolTarget *= Quaternion.Euler(0f, stepRotation, 0f);
                    break;
                case AIPatrolUnitStates.Chasing:
                    break;
                case AIPatrolUnitStates.Waiting:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnStateStay(AIPatrolUnitStates state)
        {
            Quaternion deltaRotation;
            switch (state)
            {
                case AIPatrolUnitStates.Patrol:
                    deltaRotation = Quaternion.RotateTowards(transform.rotation, patrolTarget,
                        patrolAngularSpeed * Time.deltaTime);
                    transform.rotation = deltaRotation;
                    break;
                case AIPatrolUnitStates.Chasing:
                    var targetRotation = Quaternion.LookRotation(chaseTarget.position - transform.position);
                    deltaRotation = Quaternion.RotateTowards(transform.rotation, targetRotation,
                        chaseAngularSpeed * Time.deltaTime);
                    transform.rotation = deltaRotation;
                    weapon.TryShoot(chaseTarget.position);
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
        Waiting
    }
}