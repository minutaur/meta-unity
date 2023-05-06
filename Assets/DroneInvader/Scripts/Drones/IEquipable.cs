using UnityEngine;

namespace DroneInvader.Scripts
{
    public interface IEquipable
    {
        void OnEquip(Transform socket);
        void UpdateItem();
        void OnUnEquip();
    }
}
