using Player;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [RequireComponent(typeof (Image))]
    public class BloodOverlay : MonoBehaviour
    {
        private Color baseColor;
        private PlayerCharacter playerCharacter;
        private Image image;

        private void Awake()
        {
            playerCharacter = GameObject.FindWithTag("Player").GetComponent<PlayerCharacter>();

            image = GetComponent<Image>();
        }

        private void Start()
        {
            baseColor = image.material.color;
        }

        private void Update()
        {
            baseColor.a = 1f - playerCharacter.Health / playerCharacter.maxHealth;
            baseColor.a = Mathf.Clamp01(baseColor.a);
            image.material.color = baseColor;
        }

        private void OnDestroy()
        {
            baseColor.a = 0f;
            image.material.color = baseColor;
        }
    }
}