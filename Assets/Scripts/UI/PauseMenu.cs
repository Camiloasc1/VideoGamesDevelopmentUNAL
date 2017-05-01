using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{

	public Transform pausePanel;
	public Transform optionsPanel;

	public void toggleOptions ()
	{
		if (optionsPanel.gameObject.activeInHierarchy == false) {
			pausePanel.gameObject.SetActive (false);
			optionsPanel.gameObject.SetActive (true);
		} else {
			optionsPanel.gameObject.SetActive (false);
			pausePanel.gameObject.SetActive (true);
		}
	}
}