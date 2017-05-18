using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

namespace UI.Menus
{
    public class MainMenu : MonoBehaviour
    {
        public Transform mainPanel;
        public Selectable mainFocus;
        public Transform settingsPanel;
        public Selectable settingsFocus;
        public Transform levelsPanel;
        public Selectable levelsFocus;
        public Transform creditsPanel;
        public Selectable creditsFocus;

        public MainViews CurrentView
        {
            get
            {
                if (mainPanel.gameObject.activeSelf)
                {
                    return MainViews.Main;
                }
                if (settingsPanel.gameObject.activeSelf)
                {
                    return MainViews.Settings;
                }
                if (levelsPanel.gameObject.activeSelf)
                {
                    return MainViews.Levels;
                }
                if (creditsPanel.gameObject.activeSelf)
                {
                    return MainViews.Credits;
                }
                return MainViews.Main;
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
                    case MainViews.Main:
                        gameObject.SetActive(false);
                        break;
                    case MainViews.Settings:
                        SetViewMain();
                        break;
                    case MainViews.Levels:
                        SetViewMain();
                        break;
                    case MainViews.Credits:
                        SetViewMain();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        public void SetViewMain()
        {
            SetView(MainViews.Main);
        }

        public void SetViewSettings()
        {
            SetView(MainViews.Settings);
        }

        public void SetViewLevels()
        {
            SetView(MainViews.Levels);
        }

        public void SetViewCredits()
        {
            SetView(MainViews.Credits);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

        private void SetView(MainViews view)
        {
            switch (view)
            {
                case MainViews.Main:
                    mainPanel.gameObject.SetActive(true);
                    settingsPanel.gameObject.SetActive(false);
                    levelsPanel.gameObject.SetActive(false);
                    creditsPanel.gameObject.SetActive(false);
                    mainFocus.Select();
                    break;
                case MainViews.Settings:
                    mainPanel.gameObject.SetActive(false);
                    settingsPanel.gameObject.SetActive(true);
                    levelsPanel.gameObject.SetActive(false);
                    creditsPanel.gameObject.SetActive(false);
                    settingsFocus.Select();
                    break;
                case MainViews.Levels:
                    mainPanel.gameObject.SetActive(false);
                    settingsPanel.gameObject.SetActive(false);
                    levelsPanel.gameObject.SetActive(true);
                    creditsPanel.gameObject.SetActive(false);
                    levelsFocus.Select();
                    break;
                case MainViews.Credits:
                    mainPanel.gameObject.SetActive(false);
                    settingsPanel.gameObject.SetActive(false);
                    levelsPanel.gameObject.SetActive(false);
                    creditsPanel.gameObject.SetActive(true);
                    creditsFocus.Select();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("view", view, null);
            }
        }
    }

    public enum MainViews
    {
        Main,
        Settings,
        Levels,
        Credits
    }
}