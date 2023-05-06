using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DroneInvader.Scripts
{
    public class MovingPlatform : MonoBehaviour
    {
        public float moveSpeed = 3f;
        public float waitTime = 1f;

        [SerializeField] private Transform platform;
        [SerializeField] private List<Transform> points;
        [SerializeField] private int currentIndex;
    
        private Vector3 _prevPos;
    
        private float _lerpTime;
        private float _timeToNext;
        private bool _isMoving;

        private void Start()
        {
            Transform waypoints = transform.Find("Waypoints");
            for (int i = 0; i < waypoints.childCount; i++)
                points.Add(waypoints.GetChild(i));

            platform = GetComponentInChildren<PlatformBehavior>().transform;

            StartCoroutine(PlatformMove());
        }
        private void FixedUpdate()
        {
            if (!_isMoving)
                return;

            _lerpTime += Time.fixedDeltaTime;
            platform.position = Vector3.Lerp(_prevPos, points[currentIndex].position, _lerpTime / _timeToNext);

            if (_lerpTime >= _timeToNext)
            {
                _isMoving = false;
                StartCoroutine(PlatformMove());
            }
        }
    
        IEnumerator PlatformMove()
        {
            _lerpTime = 0;
            platform.position = points[currentIndex].position;
            _prevPos = platform.position;
        
            yield return new WaitForSeconds(waitTime);
        
            currentIndex = (currentIndex + 1) % points.Count;
            _timeToNext = Vector3.Distance(_prevPos, points[currentIndex].position) / moveSpeed;

            _isMoving = true;
        }
    }
}