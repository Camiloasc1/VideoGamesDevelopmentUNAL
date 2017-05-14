using System;
using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Text))]
    public class RemainingTime : MonoBehaviour
    {
        private LevelTimer levelTimer;
        private Text timeText;

        private void Awake()
        {
            levelTimer = GameController.Instance.GetComponent<LevelTimer>();
            if (!levelTimer)
            {
                throw new ArgumentNullException("levelTimer", "levelTimer not found in GameController");
            }
            timeText = GetComponent<Text>();
        }

        private void Update()
        {
            if (!levelTimer)
            {
                return;
            }
            timeText.text = levelTimer.TimeString;
        }
    }
}