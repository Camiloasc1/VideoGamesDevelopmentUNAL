using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class EnemyCharacter : MonoBehaviour
    {
        private Rigidbody rigidBody;
        private Animator animator;

        private void Start()
        {
            animator = GetComponent<Animator>();
            rigidBody = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            animator.SetFloat("Speed", rigidBody.velocity.magnitude, 0.1f, Time.deltaTime);
            animator.SetBool("IsAiming", false);
            animator.SetBool("IsFiring", false);
            animator.SetTrigger("Reload");
        }
    }
}