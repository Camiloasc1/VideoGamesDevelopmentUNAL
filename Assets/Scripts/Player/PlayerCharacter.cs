using Game;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerCharacter : MonoBehaviour
    {
        [Tooltip("The player's max health")] public float maxHealth = 5f;
        public PlayerSounds playerSounds;

        private AudioSource audioSource;

        public delegate void OnPlayerRecieveDamage_(float damage);

        public delegate void OnPlayerDeath_();

        public event OnPlayerRecieveDamage_ OnPlayerRecieveDamage = delegate { };
        public event OnPlayerDeath_ OnPlayerDeath = delegate { };

        public float Health { get; private set; }

        public float HealthPercent
        {
            get { return Health / maxHealth; }
        }

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            Health = maxHealth;
        }

        private void PlaySound(AudioClip clip)
        {
            if (!clip)
            {
                return;
            }

            audioSource.clip = clip;
            audioSource.Play();
        }

        private void PlaySound(AudioClip[] clips)
        {
            if (clips.Length == 0)
            {
                return;
            }

            PlaySound(clips[Random.Range(0, clips.Length)]);
        }

        public void Damage(float damage)
        {
            if (Health > 0f)
            {
                Health -= damage;
                PlaySound(playerSounds.damage);
                OnPlayerRecieveDamage(damage);
                if (Health <= 0f)
                {
                    Die();
                    PlaySound(playerSounds.death);
                    OnPlayerDeath();
                }
            }
        }

        private void Die()
        {
            GameController.GameMode.GameOver();
        }
    }

    [System.Serializable]
    public struct PlayerSounds
    {
        [Tooltip("Hurt sounds")] public AudioClip[] damage;
        [Tooltip("Death sounds")] public AudioClip[] death;
        [Tooltip("Foot steps sounds")] public AudioClip[] footSteps;
        [Tooltip("Low health sound")] public AudioClip lowHealth;
    }
}