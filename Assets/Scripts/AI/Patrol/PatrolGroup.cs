using UnityEngine;

namespace AI.Patrol
{
    public class PatrolGroup : MonoBehaviour
    {
        [HideInInspector][Tooltip("How many patrol units can see the current target")] public int onView;
    }
}