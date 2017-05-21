using Game;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerCharacter : MonoBehaviour
    {
        [Tooltip("The player's max health")] public float maxHealth = 5f;
        public PlayerSounds playerSounds;

        private AudioSource[] audioSources;

        public delegate void OnPlayerRecieveDamage_(float damage);

        public delegate void OnPlayerDeath_();

        public event OnPlayerRecieveDamage_ OnPlayerRecieveDamage = delegate { };
        public event OnPlayerDeath_ OnPlayerDeath = delegate { };

        public float Health { get; private set; }

        public float HealthPercent
        {
            get { return Health / maxHealth; }
        }

        public bool IsAlive
        {
            get { return Health > 0f; }
        }

        private void Awake()
        {
            audioSources = GetComponents<AudioSource>();
        }

        private void Start()
        {
            Health = maxHealth;
        }

        private void Update()
        {
            if (HealthPercent <= 0.5f)
            {
                if (IsAlive)
                {
                    if (!audioSources[1].isPlaying)
                    {
                        audioSources[1].loop = true;
                        PlaySound(1, playerSounds.lowHealth);
                    }
                }
                else if (audioSources[1].isPlaying)
                {
                    audioSources[1].Stop();
                }
            }
            else if (audioSources[1].isPlaying)
            {
                audioSources[1].Stop();
            }
        }

        private void PlaySound(int index, AudioClip clip)
        {
            if (!clip)
            {
                return;
            }

            audioSources[index].clip = clip;
            audioSources[index].Play();
        }

        private void PlaySound(int index, AudioClip[] clips)
        {
            if (clips.Length == 0)
            {
                return;
            }

            PlaySound(index, clips[Random.Range(0, clips.Length)]);
        }

        public void Damage(float damage)
        {
            if (Health > 0f)
            {
                Health -= damage;
                PlaySound(0, playerSounds.damage);
                OnPlayerRecieveDamage(damage);
                if (Health <= 0f)
                {
                    Die();
                    PlaySound(0, playerSounds.death);
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