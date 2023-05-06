using System;
using UnityEngine;

namespace DroneInvader.Scripts
{
    public class PlayerInventory : MonoBehaviour
    {
        public Transform socket;
        public IEquipable equippedItem;
        
        public void EquipItem(IEquipable item)
        {
            if (equippedItem != null)
            {
                UnEquip();
            }
            equippedItem = item;
            equippedItem.OnEquip(socket);
        }
        public void UnEquip()
        {
            if (equippedItem != null)
            {
                equippedItem.OnUnEquip();
                equippedItem = null;
            }
        }
        private void Update()
        {
            if (equippedItem != null)
                equippedItem.UpdateItem();
        }
    }
}
