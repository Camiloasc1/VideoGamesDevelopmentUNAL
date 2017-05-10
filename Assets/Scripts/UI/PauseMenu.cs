using UnityEngine;

namespace UI
{
    public class PauseMenu : MonoBehaviour
    {
        public Transform pausePanel;
        public Transform optionsPanel;

        public void ToggleOptions()
        {
            if (optionsPanel.gameObject.activeInHierarchy)
            {
                pausePanel.gameObject.SetActive(true);
                optionsPanel.gameObject.SetActive(false);
            }
            else
            {
                pausePanel.gameObject.SetActive(false);
                optionsPanel.gameObject.SetActive(true);
            }
        }
    }
}