using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        public Transform pausePanel;
        public Selectable pauseFocus;
        public Transform settingsPanel;
        public Selectable settingsFocus;

        public Views CurrentView
        {
            get
            {
                if (pausePanel.gameObject.activeSelf)
                {
                    return Views.Main;
                }
                if (settingsPanel.gameObject.activeSelf)
                {
                    return Views.Settings;
                }
                return Views.Main;
            }
        }

        private void OnEnable()
        {
            SetViewMain();
        }

        private void Update()
        {
            if (CrossPlatformInputManager.GetButtonDown("Cancel"))
            {
                switch (CurrentView)
                {
                    case Views.Main:
                        gameObject.SetActive(false);
                        break;
                    case Views.Settings:
                        SetViewMain();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

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
                    pauseFocus.Select();
                    break;
                case Views.Settings:
                    pausePanel.gameObject.SetActive(false);
                    settingsPanel.gameObject.SetActive(true);
                    settingsFocus.Select();
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