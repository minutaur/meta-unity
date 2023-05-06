using System;
using UnityEngine;

namespace DroneInvader.Scripts
{
    public class Bullet : MonoBehaviour
    {
        public Entity parent;
        
        public float speed = 5f;
        public int damage = 15;

        private bool _didAttack;
        private Rigidbody _rigid;

        private void Start()
        {
            _rigid = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            _rigid.velocity = transform.forward * speed;
        }

        private void OnTriggerEnter(Collider other)
        {
            Entity entity = other.GetComponentInParent<Entity>();
            if (entity == parent)
                return;
            
            
            if (!_didAttack && entity)
            {
                _didAttack = true;
                entity.TakeDamage(damage, entity);
            }

            Bullet otherBullet = other.GetComponent<Bullet>();
            
            if (!otherBullet)
                Destroy(gameObject);
        }
    }
}