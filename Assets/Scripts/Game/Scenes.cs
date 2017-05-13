using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
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
}
