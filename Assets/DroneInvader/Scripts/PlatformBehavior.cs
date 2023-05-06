using UnityEngine;

namespace DroneInvader.Scripts
{
    public class PlatformBehavior : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
                other.transform.SetParent(transform);
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
                other.transform.SetParent(null);
        }
    }
}