using UnityEngine;
using UnityEngine.AI;

namespace Enemies
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyCharacter : MonoBehaviour
    {
        public float animationSpeedMultiplier = 1f;

        [HideInInspector] public bool isAiming;
        [HideInInspector] public bool isFiring;

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

        public void Reload()
        {
            animator.SetTrigger("Reload");
        }

        private void UpdateAnimator()
        {
            animator.SetFloat("Speed", agent.velocity.magnitude * animationSpeedMultiplier);
            animator.SetBool("IsAiming", isAiming);
            animator.SetBool("IsFiring", isFiring);
        }
    }
}