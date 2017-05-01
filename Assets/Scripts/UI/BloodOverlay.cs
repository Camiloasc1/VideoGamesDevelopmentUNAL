using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class BloodOverlay : MonoBehaviour
    {
        private Color baseColor;
        private PlayerCharacter playerCharacter;

        // Use this for initialization
        void Start()
        {
            playerCharacter = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();
            baseColor = GetComponent<Image>().material.color;
            print(baseColor);
        }

        // Update is called once per frame
        void Update()
        {
            baseColor.a = 1f - playerCharacter.Health / playerCharacter.maxHealth;
            baseColor.a = Mathf.Clamp01(baseColor.a);
            GetComponent<Image>().material.color = baseColor;
        }

        private void OnDestroy()
        {
            baseColor.a = 0f;
            GetComponent<Image>().material.color = baseColor;
        }
    }
}