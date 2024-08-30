using UnityEngine;
using UnityEngine.InputSystem;

namespace Components.Player_Control_Components
{
    public class FlyingControl : PlayerControl
    {
        [SerializeField]
        protected Rigidbody2D rb;
        [SerializeField]
        protected float flySpeed = 10;

        private Vector2 _flyDirection;
        
        private void FixedUpdate()
        {
            Fly();
        }
        
        protected override void MovingInputPerformed(InputAction.CallbackContext obj)
        {
            _flyDirection = obj.ReadValue<Vector2>();
            Vector2 dir = Vector2.zero;
            dir.x = _flyDirection.x;
            dir.y = _flyDirection.y;

            /*if (moveJoystick.Direction != Vector2.zero)
            {
                dir = moveJoystick.Direction;
            }*/

            //the turn part of my code
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
            
            
        }

        private void StopFly()
        {
            _flyDirection = Vector2.zero;
            
        }
    }
}
