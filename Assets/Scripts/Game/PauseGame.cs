using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
	public Transform pauseCanvas;

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Escape)) {
			if (pauseCanvas.gameObject.activeInHierarchy == false) {
				
				pauseCanvas.gameObject.SetActive (true);
				Time.timeScale = 0;
				Camera.main.GetComponent ("Blur Optimized (Script)").transform.gameObject.SetActive (true);
			} else {
				pauseCanvas.gameObject.SetActive (false);
				Time.timeScale = 1;
			}
		}
	}
}