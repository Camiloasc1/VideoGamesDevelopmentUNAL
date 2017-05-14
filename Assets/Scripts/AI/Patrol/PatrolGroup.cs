using UnityEngine;

namespace AI.Patrol
{
    public class PatrolGroup : MonoBehaviour
    {
        [HideInInspector][Tooltip("How many patrol units can see the current target")] public int onView;
        [HideInInspector][Tooltip("Current group current target")] public Transform target;
    }
}