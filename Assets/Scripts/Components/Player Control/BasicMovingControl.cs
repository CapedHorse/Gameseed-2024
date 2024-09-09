using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Components.Player_Control
{
    public class BasicMovingControl : PlayerControl
    {
        [SerializeField]
        protected Rigidbody2D rb;
        [SerializeField]
        protected float moveSpeed;

        [SerializeField] protected float playerMovingEventInterval = 0.5f;
        public UnityEvent onPlayerMovingEvent;
        
        private float _moveDirection;
        private float _playerMovedEventIntervalCounter;

        private void FixedUpdate()
        {
            FixedUpdateVirtual();
        }

        protected virtual void FixedUpdateVirtual()
        {
            Move();
        }

        protected override void MovingInputPerformed(InputAction.CallbackContext obj)
        {
            _moveDirection = obj.ReadValue<float>();
        }

        protected override void MovingInputCanceled(InputAction.CallbackContext obj)
        {
            StopMove();
        }

        private void Move()
        {
            rb.velocity = new Vector2(_moveDirection * moveSpeed, rb.velocity.y);
            if (_moveDirection != 0)
            {
                _playerMovedEventIntervalCounter += Time.fixedDeltaTime;
                if (_playerMovedEventIntervalCounter >= playerMovingEventInterval)
                {
                    _playerMovedEventIntervalCounter = 0f;
                    onPlayerMovingEvent.Invoke();
                }
            }
        }

        private void StopMove()
        {
            _moveDirection = 0;
            // rb.velocity = new Vector2(0, rb.velocity.y);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            OnEnteredCollision(other);
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            OnExitCollision(other);
        }

        protected virtual void OnEnteredCollision(Collision2D other)
        {
            if (other.collider.CompareTag("Wall"))
            {
                StopMove(); 
            }
        }

        protected virtual void OnExitCollision(Collision2D other)
        {
            
        }
    }
}
