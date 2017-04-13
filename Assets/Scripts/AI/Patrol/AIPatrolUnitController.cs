using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

namespace AI.Patrol
{
    [RequireComponent(typeof(AICharacterControl))]
    public class AIPatrolUnitController : MonoBehaviour
    {
        private AICharacterControl characterControl;
        private Transform oldTarget;

        private void Awake()
        {
            characterControl = GetComponent<AICharacterControl>();
        }

        private void OnLoSEnter(Transform target)
        {
            oldTarget = characterControl.target;
            characterControl.target = target;
        }

        private void OnLoSStay(Transform target)
        {
        }

        private void OnLoSExit(Transform target)
        {
            characterControl.target = oldTarget;
        }
    }
}