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

        public PauseViews CurrentView
        {
            get
            {
                if (pausePanel.gameObject.activeSelf)
                {
                    return PauseViews.Main;
                }
                if (settingsPanel.gameObject.activeSelf)
                {
                    return PauseViews.Settings;
                }
                return PauseViews.Main;
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
                    case PauseViews.Main:
                        gameObject.SetActive(false);
                        break;
                    case PauseViews.Settings:
                        SetViewMain();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void SetViewMain()
        {
            SetView(PauseViews.Main);
        }

        public void SetViewOptions()
        {
            SetView(PauseViews.Settings);
        }

        private void SetView(PauseViews view)
        {
            switch (view)
            {
                case PauseViews.Main:
                    pausePanel.gameObject.SetActive(true);
                    settingsPanel.gameObject.SetActive(false);
                    pauseFocus.Select();
                    break;
                case PauseViews.Settings:
                    pausePanel.gameObject.SetActive(false);
                    settingsPanel.gameObject.SetActive(true);
                    settingsFocus.Select();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("view", view, null);
            }
        }
    }

    public enum PauseViews
    {
        Main,
        Settings
    }
}