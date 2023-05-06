using UnityEngine;
using UnityEngine.Events;

namespace DroneInvader.Scripts
{
    public enum InteractType
    {
        Ray, Sphere
    }
    public abstract class Interactable : MonoBehaviour
    {
        public InteractType allowType; 
        public abstract void Interact();
    }
}