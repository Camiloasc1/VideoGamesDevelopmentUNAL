using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UI
{
    public class PauseGame : MonoBehaviour
    {
        public PauseMenu pauseMenu;

        private void Update()
        {
            if (CrossPlatformInputManager.GetButtonDown("Pause"))
            {
                TogglePause();
            }
        }

        public void TogglePause()
        {
            pauseMenu.gameObject.SetActive(!pauseMenu.gameObject.activeSelf);
        }
    }
}