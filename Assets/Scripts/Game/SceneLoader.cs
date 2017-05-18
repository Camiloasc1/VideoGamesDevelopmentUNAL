using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class SceneLoader : MonoBehaviour
    {
        public void LoadMainMenu()
        {
            LoadScene(0);
        }

        public void LoadMainScene()
        {
            LoadScene(2);
        }

        public void LoadScene(int scene)
        {
            DontDestroyOnLoad(gameObject);
            StartCoroutine(LoadAsync(scene));
        }

        private IEnumerator LoadAsync(int scene)
        {
            yield return SceneManager.LoadSceneAsync(1);
            yield return SceneManager.LoadSceneAsync(scene);
            Destroy(gameObject);
        }
    }
}