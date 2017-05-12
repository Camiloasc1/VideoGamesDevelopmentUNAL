using System;
using UnityEngine;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        public Transform pausePanel;
        public Transform optionsPanel;

        public void SetViewMain()
        {
            SetView(Views.Main);
        }

        public void SetViewOptions()
        {
            SetView(Views.Options);
        }

        private void SetView(Views view)
        {
            switch (view)
            {
                case Views.Main:
                    pausePanel.gameObject.SetActive(true);
                    optionsPanel.gameObject.SetActive(false);
                    break;
                case Views.Options:
                    pausePanel.gameObject.SetActive(false);
                    optionsPanel.gameObject.SetActive(true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException("view", view, null);
            }
        }
    }

    public enum Views
    {
        Main,
        Options
    }
}