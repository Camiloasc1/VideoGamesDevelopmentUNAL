using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scenes : MonoBehaviour {

	public void LoadMainMenu()
	{
		SceneManager.LoadScene(0);
	}
	public void LoadMainScene()
	{
		SceneManager.LoadScene(1);
	}
}
