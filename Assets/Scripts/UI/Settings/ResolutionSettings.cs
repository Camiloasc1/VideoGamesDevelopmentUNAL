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

            resolutions = Screen.resolutions;
            if (resolutions.Length == 0)
            {
                // Not supported by platform
                transform.parent.gameObject.SetActive(false);
                return;
            }
            resolutions = resolutions.Distinct().ToArray();
        }

        private void OnEnable()
        {
            dropdown.options.Clear();
            var found = false;
            for (var i = 0; i < resolutions.Length; i++)
            {
                var resolution = resolutions[i];
                dropdown.options.Add(new Dropdown.OptionData(resolution.width + "x" + resolution.height));

//                if (resolution.width == Screen.currentResolution.width &&
//                    resolution.height == Screen.currentResolution.height)
                if (Screen.currentResolution.Equals(resolution))
                {
                    dropdown.value = i;
                    found = true;
                }
            }
            if (!found)
            {
                dropdown.value = 0;
            }
        }

        public void OnValueChaged(int value)
        {
            Screen.SetResolution(resolutions[value].width, resolutions[value].height, true);
        }
    }
}