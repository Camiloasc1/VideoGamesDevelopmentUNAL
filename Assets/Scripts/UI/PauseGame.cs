using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.ImageEffects;

public class PauseGame : MonoBehaviour
{
	public Transform pauseCanvas;

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			togglePause ();
		}
	}

	public void togglePause ()
	{
		if (pauseCanvas.gameObject.activeInHierarchy == false) {

			pauseCanvas.gameObject.SetActive (true);
			Time.timeScale = 0;
			Camera.main.GetComponent<BlurOptimized> ().enabled = true;
		} else {
			pauseCanvas.gameObject.SetActive (false);
			Time.timeScale = 1;
			Camera.main.GetComponent<BlurOptimized> ().enabled = false;
		}
	}
}