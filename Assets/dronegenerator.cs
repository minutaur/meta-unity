using UnityEngine;
using UnityEngine.AI;

namespace DroneInvader.Scripts
{
    public class dronegenerator : MonoBehaviour
    {
        public GameObject prefab;
        public Vector2 spawncenter;
        public Vector2 spawnAreaSize;

        public float minSpawnInterval = 1.0f;
        public float maxSpawnInterval = 3.0f;

        private float nextSpawnTime;

        public Transform playerTarget;

        private void Start()
        {

            nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
        }

        private void Update()
        {

            if (Time.time >= nextSpawnTime)
            {

                Vector3 randomPosition = new Vector3(
                    Random.Range(spawncenter.x - spawnAreaSize.x / 2, spawncenter.x + spawnAreaSize.x / 2),
                    0,
                    Random.Range(spawncenter.y - spawnAreaSize.y / 2, spawncenter.y + spawnAreaSize.y / 2)
                );

                if (NavMesh.SamplePosition(randomPosition, out var hit, Mathf.Infinity, LayerMask.NameToLayer("Default")))
                {
                    DroneController droneController = Instantiate(prefab, hit.position, Quaternion.identity).GetComponent<DroneController>();
                    droneController.target = playerTarget;
                } else
                {
                    DroneController droneController = Instantiate(prefab, randomPosition, Quaternion.identity).GetComponent<DroneController>();
                    droneController.target = playerTarget;
                }

                nextSpawnTime = Time.time + Random.Range(minSpawnInterval, maxSpawnInterval);
            }
        }
    }
}