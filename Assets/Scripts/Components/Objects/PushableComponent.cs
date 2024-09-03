using System;
using Components.ExtraComponents;
using UnityEngine;

namespace Components.Objects
{
    public class PushableComponent : ComponentBase
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float lerpTime = 1f;
        [SerializeField] private float lerpTimeDivider = 25f;
        private bool _pushed;
        private Vector2 _targetPushedLoc;
        private float _currentLerpTime;

        private void FixedUpdate()
        {
            if (!_pushed)
                return;

            _currentLerpTime -= Time.fixedDeltaTime;
            transform.position = Vector2.Lerp(transform.position, _targetPushedLoc, _currentLerpTime/lerpTimeDivider);

            if (_currentLerpTime >= lerpTime)
            {
                _pushed = false;
                _currentLerpTime = lerpTime;
            }
        }

        protected override void EnteredCollision(Collision2D other)
        {
            PushingComponent pusher = other.gameObject.GetComponent<PushingComponent>();

            if (pusher)
            {
                // Calculate Angle Between the collision point and the player
                Vector2 dir = other.GetContact(0).point - (Vector2) transform.position;
                float xDistFromCenter = Mathf.Abs(dir.x);
                float yDistFromCenter = Mathf.Abs(dir.y);
                dir = -dir.normalized;
                float xDirSign = Mathf.Sign(dir.x);
                float yDirSign = Mathf.Sign(dir.y);
                // And finally we add force in the direction of dir and multiply it by force. 
                // This will push back the player
                
                _targetPushedLoc = transform.position;
                _targetPushedLoc.x += xDistFromCenter >= yDistFromCenter ? xDirSign * pusher.PushingForce/10 : 0;
                _targetPushedLoc.y += yDistFromCenter >= xDistFromCenter ? yDirSign * pusher.PushingForce/10 : 0;
                _pushed = true;
                _currentLerpTime = lerpTime;
            }
        }
    }
}
