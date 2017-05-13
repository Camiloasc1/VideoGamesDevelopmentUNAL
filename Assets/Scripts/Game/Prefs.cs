using UnityEngine;
using UnityEngine.Audio;

namespace Game
{
    public class Prefs : MonoBehaviour
    {
        public AudioMixer audioMixer;

        private void Start()
        {
            ReloadPlayerPrefs();
        }

        private void ReloadPlayerPrefs()
        {
            audioMixer.SetFloat("FXVolume", PlayerPrefs.GetFloat("FXVolume"));
            audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));
        }

        public void SetFXVolume(float value)
        {
            PlayerPrefs.SetFloat("FXVolume", value);
            audioMixer.SetFloat("FXVolume", value);
        }

        public void SetMusicVolume(float value)
        {
            PlayerPrefs.SetFloat("MusicVolume", value);
            audioMixer.SetFloat("MusicVolume", value);
        }
    }
}