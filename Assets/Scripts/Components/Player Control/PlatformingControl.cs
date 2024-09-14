using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Components.Player_Control
{
    public class PlatformingControl : BasicMovingControl
    {
        [SerializeField] private float jumpPower = 0.5f;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundCheckRadius = 5f;
        [SerializeField] private LayerMask groundLayerMask;
        public UnityEvent onPlayerJumpEvent;

        public bool HasJumped => _hasJumped;
        private bool _hasJumped;
        
        protected override void FixedUpdateVirtual()
        {
            base.FixedUpdateVirtual();
            
            _hasJumped = !Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayerMask);
        }

        protected override void MainActionInputStarted(InputAction.CallbackContext obj)
        {
            if (!playerInput.inputIsActive)
                return;
            
            if (_hasJumped)
                return;

            // _hasJumped = true;
            rb.AddForce(Vector2.up * jumpPower);
            onPlayerJumpEvent.Invoke();
        }

    }
}