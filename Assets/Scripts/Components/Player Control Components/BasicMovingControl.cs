using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Components.Player_Control_Components
{
    public class BasicMovingControl : PlayerControl
    {
        [SerializeField]
        protected Rigidbody2D rb;
        [SerializeField]
        protected float moveSpeed;

        private float _moveDirection;

        private void FixedUpdate()
        {
            Move();
        }

        protected override void MovingInputPerformed(InputAction.CallbackContext obj)
        {
            _moveDirection = obj.ReadValue<float>();
            // rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        }

        protected override void MovingInputCanceled(InputAction.CallbackContext obj)
        {
            StopMove();
        }

        private void Move()
        {
            rb.velocity = new Vector2(_moveDirection * moveSpeed, rb.velocity.y);
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

        protected virtual void OnEnteredCollision(Collision2D other)
        {
            if (other.collider.CompareTag("Wall"))
            {
                StopMove(); 
            }
        }
    }
}
