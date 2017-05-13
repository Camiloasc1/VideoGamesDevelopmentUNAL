using UnityEngine;
using UnityEngine.UI;

namespace UI.Settings
{
    [RequireComponent(typeof(Toggle))]
    public class FullScreen : MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Toggle>().isOn = Screen.fullScreen;
        }

        public void SetFullScreen(bool fullScreen)
        {
            Screen.fullScreen = fullScreen;
        }
    }
}