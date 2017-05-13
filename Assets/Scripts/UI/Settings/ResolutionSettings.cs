using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Settings
{
    [RequireComponent(typeof(Dropdown))]
    public class ResolutionSettings : MonoBehaviour
    {
        private Dropdown dropdown;
        private Resolution[] resolutions;

        private void Awake()
        {
            dropdown = GetComponent<Dropdown>();
        }

        private void Start()
        {
            if (!FillAvailableResolutions())
            {
                return;
            }

            FillResolutionOptions();
        }

        public void OnValueChaged(int value)
        {
            Screen.SetResolution(resolutions[value].width, resolutions[value].height, true);
        }

        private void FillResolutionOptions()
        {
            dropdown.options.Clear();
            for (var i = 0; i < resolutions.Length; i++)
            {
                var resolution = resolutions[i];
                dropdown.options.Add(new Dropdown.OptionData(resolution.width + "x" + resolution.height));

//                if (resolution.width == Screen.currentResolution.width &&
//                    resolution.height == Screen.currentResolution.height)
                if (Screen.currentResolution.Equals(resolution))
                {
                    dropdown.value = i;
                }
            }
            dropdown.RefreshShownValue();
        }

        private bool FillAvailableResolutions()
        {
            resolutions = Screen.resolutions;
            if (resolutions.Length == 0)
            {
                // Not supported by platform
                transform.parent.gameObject.SetActive(false);
                return false;
            }
            resolutions = resolutions.Distinct().ToArray();
            return true;
        }
    }
}