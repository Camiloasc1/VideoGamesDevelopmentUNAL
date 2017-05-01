using UnityEngine;

namespace Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        [Tooltip("The player's max health")] public float maxHealth = 5f;

        public float Health { get; private set; }

        private void Start()
        {
            Health = maxHealth;
        }

        public bool Damage(float damage)
        {
            if (Health > 0f)
            {
                Health -= damage;
                if (Health <= 0f)
                {
                    Die();
                    return true;
                }
                return false;
            }
            return true;
        }

        private void Die()
        {
            throw new System.NotImplementedException();
        }
    }
}