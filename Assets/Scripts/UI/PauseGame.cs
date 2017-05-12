using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UI
{
    public class PauseGame : MonoBehaviour
    {
        public GameObject pauseMenuCanvas;

        private void Update()
        {
            if (CrossPlatformInputManager.GetButtonDown("Pause"))
            {
                TogglePause();
            }
        }

        public void TogglePause()
        {
            pauseMenuCanvas.SetActive(!pauseMenuCanvas.activeSelf);
        }
    }
}