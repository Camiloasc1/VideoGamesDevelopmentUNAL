using Game;
using UnityEngine;

namespace Player
{
    public class PlayerCharacter : MonoBehaviour
    {
        [Tooltip("The player's max health")] public float maxHealth = 5f;

        public delegate void OnPlayerRecieveDamage_(float damage);
        public delegate void OnPlayerDeath_();

        public event OnPlayerRecieveDamage_ OnPlayerRecieveDamage = delegate { };
        public event OnPlayerDeath_ OnPlayerDeath = delegate { };

        public float Health { get; private set; }

        public float HealthPercent
        {
            get { return Health / maxHealth; }
        }

        private void Start()
        {
            Health = maxHealth;
        }

        public void Damage(float damage)
        {
            if (Health > 0f)
            {
                Health -= damage;
                OnPlayerRecieveDamage(damage);
                if (Health <= 0f)
                {
                    Die();
                    OnPlayerDeath();
                }
            }
        }

        private void Die()
        {
            GameController.GameMode.GameOver();
        }
    }
}