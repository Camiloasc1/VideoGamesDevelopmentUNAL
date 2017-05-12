using UnityEngine;

namespace Game
{
    [RequireComponent(typeof(Collider))]
    public class VictoryTarget : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                print("Victory!");
                throw new System.NotImplementedException();
            }
        }
    }
}