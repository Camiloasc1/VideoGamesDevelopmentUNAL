using System;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace AI.Patrol
{
    [RequireComponent(typeof(AICharacterControl))]
    public class AIPatrolUnitController : MonoBehaviour
    {
        private NavMeshAgent navAgent;
        private AICharacterControl characterControl;

        private Transform patrolTarget;
        private Transform chaseTarget;
        private AIPatrolUnitStates state;

        private void Awake()
        {
            navAgent = GetComponentInChildren<NavMeshAgent>();
            characterControl = GetComponent<AICharacterControl>();
        }

        private void Start()
        {
            patrolTarget = characterControl.target;
        }

        private void Update()
        {
            // Update the current state and act accordingly
            if (UpdateState())
            {
                OnStateChange();
            }

            // Execute the state
            ExecuteState();
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

        private bool UpdateState()
        {
            switch (state)
            {
                case AIPatrolUnitStates.Patrol:
                    if (chaseTarget != null)
                    {
                        state = AIPatrolUnitStates.Chasing;
                        return true;
                    }
                    break;
                case AIPatrolUnitStates.Chasing:
                    if (chaseTarget == null)
                    {
                        state = AIPatrolUnitStates.Lost;
                        return true;
                    }
                    break;
                case AIPatrolUnitStates.Lost:
                    if (chaseTarget != null)
                    {
                        state = AIPatrolUnitStates.Chasing;
                        return true;
                    }
                    else if (navAgent.remainingDistance < navAgent.stoppingDistance)
                    {
                        state = AIPatrolUnitStates.Patrol;
                        return true;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return false;
        }

        private void OnStateChange()
        {
            switch (state)
            {
                case AIPatrolUnitStates.Patrol:
                    characterControl.target = patrolTarget;
                    break;
                case AIPatrolUnitStates.Chasing:
                    characterControl.target = chaseTarget;
                    break;
                case AIPatrolUnitStates.Lost:
                    characterControl.target = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ExecuteState()
        {
            switch (state)
            {
                case AIPatrolUnitStates.Patrol:
                    break;
                case AIPatrolUnitStates.Chasing:
                    break;
                case AIPatrolUnitStates.Lost:
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
        Lost
    }
}