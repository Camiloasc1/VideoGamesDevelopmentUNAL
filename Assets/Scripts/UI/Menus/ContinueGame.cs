using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI.Menus
{
    public class ContinueGame : MonoBehaviour
    {
        public void Continue()
        {
            int savedLevel;
            if (PlayerPrefs.HasKey("SavedLevel"))
            {
                savedLevel = PlayerPrefs.GetInt("SavedLevel");
                if (savedLevel == 0)
                {
                    savedLevel = 1;
                }
            }
            else
            {
                savedLevel = 1;
            }
            SceneManager.LoadScene(savedLevel);
        }
    }
}