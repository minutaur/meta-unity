using UnityEngine;
using UnityEngine.AI;

namespace DroneInvader.Scripts
{
    public class DroneController : MonoBehaviour
    {
        public Bullet bulletPrefab;
        public Transform firePos;
        public Transform target;
        
        public float fireRate = 1f;
        public float fireRange = 15f;

        public bool canMoveVertically = true;
        
        private float _nextFire;
        private PlayerEntity _player;
        private NavMeshAgent _agent;
        
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _player = FindObjectOfType<PlayerEntity>();
        }

        private void Update()
        {
            _agent.destination = target.position;
            Vector3 dirToPlayer = _player.transform.position - transform.position;
            if (!canMoveVertically)
            {
                dirToPlayer.y = 0;
            }
            transform.rotation = Quaternion.LookRotation(dirToPlayer);

            if (Time.time > _nextFire && IsPlayerInRange())
                Fire();
        }

        bool IsPlayerInRange()
        {
            return Vector3.Distance(transform.position, _player.transform.position) < fireRange;
        }

        void Fire()
        {
            _nextFire = fireRate + Time.time;

            Bullet go = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            go.parent = GetComponent<Entity>();
            Destroy(go.gameObject, 10f);
            
        }
    }
}