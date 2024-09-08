using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Components.Player_Control
{
    public class PlatformingControl : BasicMovingControl
    {
        [SerializeField] private float jumpPower = 0.5f;
        public UnityEvent onPlayerJumpEvent;

        public bool HasJumped => _hasJumped;
        private bool _hasJumped;

        

        protected override void MainActionInputStarted(InputAction.CallbackContext obj)
        {
            if (_hasJumped)
                return;
            
            rb.AddForce(Vector2.up * jumpPower);
            onPlayerJumpEvent.Invoke();
            
        }

        protected override void OnEnteredCollision(Collision2D other)
        {
            base.OnEnteredCollision(other);

            if (other.collider.CompareTag("Floor"))
            {
                _hasJumped = false;
            }
        }

        protected override void OnExitCollision(Collision2D other)
        {
            base.OnExitCollision(other);
            
            if (other.collider.CompareTag("Floor"))
            {
                _hasJumped = true;
            }
        }
    }
}