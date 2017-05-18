using Game;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menus
{
    public class ContinueGame : MonoBehaviour
    {
        public SceneLoader sceneLoader;

        public void Continue()
        {
            int savedLevel;
            if (PlayerPrefs.HasKey("SavedLevel"))
            {
                savedLevel = PlayerPrefs.GetInt("SavedLevel");
                if (savedLevel == 0)
                {
                    savedLevel = 2;
                }
            }
            else
            {
                savedLevel = 2;
            }
            sceneLoader.LoadScene(savedLevel);
        }
    }
}