using TMPro;
using UnityEngine;

namespace DroneInvader.Scripts
{
    public class KillInfo : MonoBehaviour
    {
        public TextMeshProUGUI killer, victim;
        
        public void SetInfo(Entity killer, Entity victim)
        {
            this.killer.text = killer.name;
            this.victim.text = victim.name;
        }
    }
}
