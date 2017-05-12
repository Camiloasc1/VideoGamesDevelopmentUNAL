using System;
using UnityEngine;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        public Transform pausePanel;
        public Transform settingsPanel;

        public void SetViewMain()
        {
            SetView(Views.Main);
        }

        public void SetViewOptions()
        {
            SetView(Views.Settings);
        }

        private void SetView(Views view)
        {
            switch (view)
            {
                case Views.Main:
                    pausePanel.gameObject.SetActive(true);
                    settingsPanel.gameObject.SetActive(false);
                    break;
                case Views.Settings:
                    pausePanel.gameObject.SetActive(false);
                    settingsPanel.gameObject.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("view", view, null);
            }
        }
    }

    public enum Views
    {
        Main,
        Settings
    }
}