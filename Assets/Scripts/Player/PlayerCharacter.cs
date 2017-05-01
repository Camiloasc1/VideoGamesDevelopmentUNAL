using UnityEngine;

namespace Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        [Tooltip("The player's max health")] public float maxHealth = 5f;

        public float Health { get; private set; }

        // Use this for initialization
        private void Start()
        {
            Health = maxHealth;
        }

        public bool Damage(float damage)
        {
            Health -= damage;
            print(Health);
            if (Health <= 0f)
            {
                Die();
                return true;
            }
            return false;
        }

        private void Die()
        {
            throw new System.NotImplementedException();
        }
    }
}