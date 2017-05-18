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
            LoadScene(1);
        }

        public void LoadScene(int scene)
        {
            StartCoroutine(LoadAsync(scene));
        }

        private static IEnumerator LoadAsync(int scene)
        {
            var loading = SceneManager.LoadSceneAsync(scene);
            yield return loading;
        }
    }
}