using UnityEngine;
using UnityEngine.Audio;

namespace Game
{
    public class Prefs : MonoBehaviour
    {
        public AudioMixer audioMixer;

        public delegate void OnSettingsChanged_();

        public event OnSettingsChanged_ OnSettingsChanged = delegate { };

        private void Start()
        {
            ReloadPlayerPrefs();
        }

        public void ResetPlayerSettings()
        {
            PlayerPrefs.SetFloat("FXVolume", 0);
            PlayerPrefs.SetFloat("MusicVolume", 0);

            ReloadPlayerPrefs();
        }

        public void ReloadPlayerPrefs()
        {
            audioMixer.SetFloat("FXVolume", PlayerPrefs.GetFloat("FXVolume"));
            audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));

            OnSettingsChanged();
        }

        public void SetFXVolume(float value)
        {
            PlayerPrefs.SetFloat("FXVolume", value);
            ReloadPlayerPrefs();
        }

        public void SetMusicVolume(float value)
        {
            PlayerPrefs.SetFloat("MusicVolume", value);
            ReloadPlayerPrefs();
        }
    }
}