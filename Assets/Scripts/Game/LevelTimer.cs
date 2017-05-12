using System;
using UnityEngine;

namespace Game
{
    public class LevelTimer : MonoBehaviour
    {
        [Tooltip("The level's time limit")] public float timeLimit;

        public float TimeLeft { get; private set; }

        public string TimeString
        {
            get
            {
                var min = Mathf.FloorToInt(TimeLeft / 60f);
                var sec = Mathf.FloorToInt(TimeLeft) - min * 60;
                return string.Format("{0:00}:{1:00}", min, sec);
            }
        }

        private void Start()
        {
            TimeLeft = timeLimit;
        }

        private void Update()
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                if (TimeLeft <= 0)
                {
                    TimeLeft = 0;
                    GameController.GameMode.Defeat();
                }
            }
        }
    }
}