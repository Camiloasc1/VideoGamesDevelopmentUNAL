using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.ImageEffects;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        public Transform pausePanel;
        public Selectable pauseFocus;
        public Transform settingsPanel;
        public Selectable settingsFocus;
        private BlurOptimized cameraBlur;

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

        private void Awake()
        {
            cameraBlur = Camera.main.GetComponent<BlurOptimized>();
        }

        private void OnEnable()
        {
            if (cameraBlur)
            {
                cameraBlur.enabled = true;
            }
            Time.timeScale = 0f;
            SetViewMain();
        }

        private void OnDisable()
        {
            if (cameraBlur)
            {
                cameraBlur.enabled = false;
            }
            Time.timeScale = 1f;
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