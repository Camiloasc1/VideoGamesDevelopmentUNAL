using UnityEngine;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        public Transform pausePanel;
        public Transform optionsPanel;

        public void ToggleOptions()
        {
            if (optionsPanel.gameObject.activeInHierarchy == false)
            {
                pausePanel.gameObject.SetActive(false);
                optionsPanel.gameObject.SetActive(true);
            }
            else
            {
                optionsPanel.gameObject.SetActive(false);
                pausePanel.gameObject.SetActive(true);
            }
        }
    }
}