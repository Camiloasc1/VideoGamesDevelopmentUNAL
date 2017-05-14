using Player;
using UnityEngine;

namespace AI
{
    public class LoSTarget : MonoBehaviour
    {
        public Vector3 crouchEyes = new Vector3(0, 0.7f, 0);
        public Vector3 standEyes = new Vector3(0, 1.33f, 0);
        private PlayerController playerController;

        private void Awake()
        {
            playerController = GetComponentInParent<PlayerController>();
        }

        private void Update()
        {
            if (playerController)
            {
                transform.position = playerController.bIsHiding ? crouchEyes : standEyes;
            }
        }
    }
}