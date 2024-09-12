using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Components.ExtraComponents
{
    public class MovingToTargetComponent : MonoBehaviour
    {
        [SerializeField] private Transform targetTransform;
        [SerializeField] private float movingSpeed = 3f;
        [SerializeField] private float movingEventInterval = 0.75f;
        [SerializeField] private bool stopMoving = true;
        public UnityEvent onMovingEvent;
        
        private float _currentMovingEventInterval;

        private void Update()
        {
            if (stopMoving)
                return;
            
            transform.position = Vector3.MoveTowards(transform.position, targetTransform.position, Time.deltaTime * movingSpeed);
            
            Vector2 dirInput = transform.position;
            float angle = Mathf.Atan2(dirInput.y, dirInput.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
            
            _currentMovingEventInterval += Time.deltaTime;
            if (_currentMovingEventInterval >= movingEventInterval)
            {
                _currentMovingEventInterval = 0;
                onMovingEvent.Invoke();
            }
        }

        public void StopMoving(bool stop)
        {
            stopMoving = stop;
        }
    }
}