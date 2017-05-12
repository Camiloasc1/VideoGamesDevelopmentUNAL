using UnityEngine;

namespace Game
{
    public class LevelTimer : MonoBehaviour
    {
        [Tooltip("The level's time limit")] public float timeLimit;

        public float TimeLeft { get; private set; }

        private void Start()
        {
            TimeLeft = timeLimit;
        }

        private void Update()
        {
            TimeLeft -= Time.deltaTime;
        }
    }
}