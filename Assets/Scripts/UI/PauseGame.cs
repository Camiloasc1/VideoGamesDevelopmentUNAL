using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.ImageEffects;

namespace UI
{
    public class PauseGame : MonoBehaviour
    {
        public Transform pauseCanvas;
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
            if (pauseCanvas.gameObject.activeInHierarchy == false)
            {
                pauseCanvas.gameObject.SetActive(true);
                Time.timeScale = 0f;
                cameraBlur.enabled = true;
            }
            else
            {
                pauseCanvas.gameObject.SetActive(false);
                Time.timeScale = 1f;
                cameraBlur.enabled = false;
            }
        }
    }
}