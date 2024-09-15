using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Components.Player_Control
{
    public class FlyingControl : PlayerControl
    {
        [SerializeField]
        protected Rigidbody2D rb;
        [SerializeField]
        protected float flySpeed = 10;

        [SerializeField] protected float playerFlyingEventInterval = 0.5f;
        public UnityEvent onPlayerFlyingEvent;
        
        private Vector2 _flyDirection;
        private float _lastXDir;
        private float _lastYDir;
        private float _playerMovedEventIntervalCounter;

        private void FixedUpdate()
        {
            Fly();
        }

        protected override void MovingInputStarted(InputAction.CallbackContext obj)
        {
           
        }

        protected override void MovingInputPerformed(InputAction.CallbackContext obj)
        {
            if (!playerInput.inputIsActive)
                return;

            if (Time.timeScale == 0)
                return;
            
            Vector2 dir = Vector2.zero;
            _flyDirection = obj.ReadValue<Vector2>();
            _flyDirection.Normalize();

            //Test lagi kalau controller kedetect yak
            if (_flyDirection == Vector2.zero)
                return;
            
            Vector2 dirInput = _flyDirection;
            dir.x = dirInput.x;
            dir.y = dirInput.y;

            float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }

        protected override void MovingInputCanceled(InputAction.CallbackContext obj)
        {
            StopFly();
        }
        
        private void Fly()
        {
            rb.velocity = _flyDirection * flySpeed;

            _playerMovedEventIntervalCounter += Time.fixedDeltaTime;
            if (_playerMovedEventIntervalCounter >= playerFlyingEventInterval)
            {
                _playerMovedEventIntervalCounter = 0f;
                onPlayerFlyingEvent.Invoke();
            }
        }

        private void StopFly()
        {
            _flyDirection = Vector2.zero;
        }
    }
}
