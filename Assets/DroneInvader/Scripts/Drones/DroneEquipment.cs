using UnityEngine;

namespace DroneInvader.Scripts
{
    public class DroneEquipment : MonoBehaviour, IEquipable
    {
        public Bullet bulletPrefab;
        public Transform firePos;
        
        public float fireRate = 1f;
        
        private Rigidbody _rigid;
        private float _nextFire;

        private void Start()
        {
            _rigid = GetComponent<Rigidbody>();
        }

        public void OnEquip(Transform socket)
        {
            _rigid.isKinematic = true;
            transform.SetParent(socket);
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }

        public void UpdateItem()
        {
            if(Time.time > _nextFire)
                Fire();
        }
        void Fire()
        {
            _nextFire = fireRate + Time.time;
            
            GameObject go = Instantiate(bulletPrefab, firePos.position, firePos.rotation).gameObject;
            Destroy(go, 10f);
        }

        public void OnUnEquip()
        {
            transform.SetParent(null);
            _rigid.isKinematic = false;
        }
    }
}

