using UnityEngine;

namespace Game
{
	public class GameMode : MonoBehaviour {

		public Transform victoryCanvas;
		public Transform gameOverCanvas;

		public void Victory()
		{
			victoryCanvas.gameObject.SetActive(true);
		}

		public void GameOver()
		{
			gameOverCanvas.gameObject.SetActive(true);
		}
	}
}
