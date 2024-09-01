using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Components.Player_Control_Components
{
    public class PlatformingControl : BasicMovingControl
    {
        [SerializeField] private float jumpPower = 0.5f;
        private bool _hasJumped;
        private bool _fallingDown;
        private TweenCallback jumpingTweener;

        private void Update()
        {
            /*if (_hasJumped)
            {
                
            }*/
        }

        protected override void MainActionInputStarted(InputAction.CallbackContext obj)
        {
            if (_hasJumped)
                return;

            _hasJumped = true;
            
            /*rb.bodyType = RigidbodyType2D.Dynamic;
            rb.gravityScale = 5;*/
            rb.AddForce(Vector2.up * jumpPower);
            
        }

        protected override void OnEnteredCollision(Collision2D other)
        {
            base.OnEnteredCollision(other);

            if (other.collider.CompareTag("Floor"))
            {
                _hasJumped = false;
                /*rb.gravityScale = 1;
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.bodyType = RigidbodyType2D.Kinematic;*/
                
                
            }
        }
    }
}