using System.Collections;
using UnityEngine;

namespace Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayRandomSound : MonoBehaviour
    {
        public bool random;
        public Vector2 pauseBetweenClips;
        public AudioClip[] clips;

        private AudioSource audioSource;
        private int current;
        private Coroutine player;

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }

        private void OnEnable()
        {
            player = StartCoroutine(PlayNext());
        }

        private void OnDisable()
        {
            StopCoroutine(player);
        }

        private IEnumerator PlayNext()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(pauseBetweenClips.x, pauseBetweenClips.y));

                if (random)
                {
                    current = Random.Range(0, clips.Length);
                }
                else
                {
                    current = ++current % clips.Length;
                }

                audioSource.clip = clips[current];
                audioSource.Play();

                yield return new WaitForSeconds(clips[current].length);
            }
        }
    }
}