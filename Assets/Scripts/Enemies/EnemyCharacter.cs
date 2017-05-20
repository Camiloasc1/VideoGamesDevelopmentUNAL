using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyCharacter : MonoBehaviour
    {
        public float animationSpeedMultiplier = 1f;

        private Animator animator;
        private NavMeshAgent agent;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            agent = GetComponent<NavMeshAgent>();
        }

        private void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            animator.SetFloat("Speed", agent.velocity.magnitude * animationSpeedMultiplier);
//            animator.SetBool("IsAiming", false);
//            animator.SetBool("IsFiring", false);
//            animator.SetTrigger("Reload");
        }
    }
}