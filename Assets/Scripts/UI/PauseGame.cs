using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.ImageEffects;

namespace UI
{
    public class PauseGame : MonoBehaviour
    {
        public Transform pauseMenuCanvas;
        private BlurOptimized cameraBlur;

        private void Awake()
        {
            cameraBlur = Camera.main.GetComponent<BlurOptimized>();
        }

        private void Update()
        {
            if (CrossPlatformInputManager.GetButtonDown("Pause"))
            {
                TogglePause();
            }
        }

        public void TogglePause()
        {
            if (pauseMenuCanvas.gameObject.activeInHierarchy)
            {
                pauseMenuCanvas.gameObject.SetActive(false);
                Time.timeScale = 1f;
                cameraBlur.enabled = false;
            }
            else
            {
                pauseMenuCanvas.gameObject.SetActive(true);
                pauseMenuCanvas.GetComponent<PauseMenu>().SetViewMain();
                Time.timeScale = 0f;
                cameraBlur.enabled = true;
            }
        }
    }
}