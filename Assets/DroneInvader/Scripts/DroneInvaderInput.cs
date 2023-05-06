using UnityEngine;
using UnityEngine.InputSystem;

namespace DroneInvader.Scripts
{
    public class DroneInvaderInput : MonoBehaviour
    {
        public bool interact;
        
        void OnInteract(InputValue value)
        {
            interact = value.isPressed;
        }
    }
}