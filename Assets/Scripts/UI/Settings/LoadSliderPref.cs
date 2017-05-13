using Game;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Settings
{
    [RequireComponent(typeof(Slider))]
    public class LoadSliderPref : MonoBehaviour
    {
        public string pref;

        private void Start()
        {
            ReloadValue();
        }

        private void OnEnable()
        {
            GameController.Prefs.OnSettingsChanged += ReloadValue;
        }

        private void OnDisable()
        {
            GameController.Prefs.OnSettingsChanged -= ReloadValue;
        }

        private void ReloadValue()
        {
            if (pref.Length == 0)
            {
                return;
            }
            GetComponent<Slider>().value = PlayerPrefs.GetFloat(pref);
        }
    }
}