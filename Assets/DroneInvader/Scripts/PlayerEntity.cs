using System;
using StarterAssets;
using UnityEngine;

namespace DroneInvader.Scripts
{
    public class PlayerEntity : Entity
    {
        public LayerMask interactableLayer;
        public bool isDead;
    
        private ThirdPersonController _controller;
        private DroneInvaderInput _input;
        private PlayerInventory _inventory;
        private Vector3 _respawnPos;
        private Transform _playerCamera;



        protected override void Start()
        {
            base.Start();
        
            _respawnPos = transform.position;
            _controller = GetComponent<ThirdPersonController>();
            _input = GetComponent<DroneInvaderInput>();
            _inventory = GetComponent<PlayerInventory>();
            _playerCamera = Camera.main.transform;
        }

        private void Update()
        {
            InteractTo();
        }

        private void InteractTo()
        {
            Debug.DrawRay(_playerCamera.position, _playerCamera.forward * 10f, Color.green);

            if (!_input.interact)
                return;
            _input.interact = false;

            Interactable interactable = default;
            if (Physics.Raycast(_playerCamera.position, _playerCamera.forward,
                    out var hit, 10f, interactableLayer))
            {
                if (hit.collider.TryGetComponent(out interactable)
                    && interactable.allowType == InteractType.Ray)
                    interactable.Interact();
                if (hit.collider.TryGetComponent(out IEquipable equipable))
                    _inventory.EquipItem(equipable);
            }

            if (!interactable)
            {
                Collider[] cols = Physics.OverlapSphere(transform.position, 2f, interactableLayer);
                if (cols.Length > 0 && cols[0].TryGetComponent(out interactable)
                    && interactable.allowType == InteractType.Sphere)
                    interactable.Interact();
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, 2f);
        }

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);

            PlayerHUDManager.Instance.SetHealth((float)curHealth / maxHealth);
        }
        public override void TakeDamage(int damage, Entity damager)
        {
            base.TakeDamage(damage, damager);

            PlayerHUDManager.Instance.SetHealth((float)curHealth / maxHealth);
        }


        protected override void Die(Entity killer)
        {
            if (isDead)
                return;

            PlayerHUDManager.Instance.ShowKillLog(this, killer);
            onDeath?.Invoke(killer);

            _controller.enabled = false;
            GetComponent<Animator>().enabled = !GetComponent<Animator>().enabled;

            isDead = true;
            Invoke(nameof(Respawn), 3f);

        }

        public void Respawn()
        {
            gameObject.SetActive(false);
            transform.SetParent(null);
            transform.position = _respawnPos;
            gameObject.SetActive(true);



            _controller.enabled = true;
            GetComponent<Animator>().enabled = !GetComponent<Animator>().enabled;
            curHealth = maxHealth;
            PlayerHUDManager.Instance.SetHealth(1f);
            isDead = false;
        }

    }
}

