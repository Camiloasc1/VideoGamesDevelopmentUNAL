using UnityEngine;
using UnityStandardAssets.ImageEffects;

namespace UI
{
	public class PauseAndBlur : MonoBehaviour {
		private BlurOptimized cameraBlur;

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
		}

		private void OnDisable()
		{
			if (cameraBlur)
			{
				cameraBlur.enabled = false;
			}
			Time.timeScale = 1f;
		}
	}
}
