using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof(Selectable))]
    public class TakeFocus : MonoBehaviour
    {
        private void OnEnable()
        {
            GetComponent<Selectable>().Select();
        }
    }
}