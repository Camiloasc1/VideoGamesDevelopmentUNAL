using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public class EnemyCharacter : MonoBehaviour
    {
        [SerializeField] float m_MovingTurnSpeed = 360;
        [SerializeField] float m_StationaryTurnSpeed = 180;
        [Range(1f, 4f)] [SerializeField] float m_GravityMultiplier = 2f;
        [SerializeField] float m_MoveSpeedMultiplier = 1f;
        [SerializeField] float m_AnimSpeedMultiplier = 1f;

        Rigidbody m_Rigidbody;
        Animator m_Animator;
        float m_TurnAmount;
        float m_ForwardAmount;


        void Start()
        {
            m_Animator = GetComponent<Animator>();
            m_Rigidbody = GetComponent<Rigidbody>();

            m_Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY |
                                      RigidbodyConstraints.FreezeRotationZ;
        }


        public void Move(Vector3 move, bool crouch, bool jump)
        {
            // convert the world relative moveInput vector into a local-relative
            // turn amount and forward amount required to head in the desired
            // direction.
            if (move.magnitude > 1f) move.Normalize();
            move = transform.InverseTransformDirection(move);
            m_TurnAmount = Mathf.Atan2(move.x, move.z);
            m_ForwardAmount = move.z;

            // send input and other state parameters to the animator
            UpdateAnimator(move);
        }

        void UpdateAnimator(Vector3 move)
        {
            // update the animator parameters
            m_Animator.SetFloat("Speed", m_ForwardAmount, 0.1f, Time.deltaTime);
            m_Animator.SetBool("IsAiming", false);
            m_Animator.SetBool("IsFiring", false);
            m_Animator.SetTrigger("Reload");
            // the anim speed multiplier allows the overall speed of walking/running to be tweaked in the inspector,
            // which affects the movement speed because of the root motion.
            if (move.magnitude > 0)
            {
                m_Animator.speed = m_AnimSpeedMultiplier;
            }
        }
    }
}