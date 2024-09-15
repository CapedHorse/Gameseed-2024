using System;
using Components.ExtraComponents;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects
{
    public class PushableComponent : ComponentBase
    {
        [SerializeField] private Rigidbody2D rb;
        public UnityEvent onPushedEvent;
        private bool _pushedEventInvoked;
        
        [SerializeField] private float pushedPower = 0.1f;
        public UnityEvent<float> onPushedValueEvent;

        private Tweener currentPushTween;

        private bool _canBePushed = true;
        
        protected override void EnteredCollision(Collision2D other)
        {
            if (other.gameObject.GetComponent<PushingComponent>())
            {
                if (!_canBePushed)
                    return;
                
                StopPush();
                
                //Watch over this ya, masih ga work, masih bisa tembus wall
                if (other.collider.CompareTag("Floor") || other.collider.CompareTag("Ceil") ||
                    other.collider.CompareTag("Wall"))
                {
                    
                    _canBePushed = false;
                    StopPush();
                    return;
                }
                
                Vector2 dir = other.GetContact(0).normal;
                Vector2 pos = transform.position;
                
                pos.x += rb.constraints == RigidbodyConstraints2D.FreezePositionX ? 0 : dir.x * pushedPower;
                pos.y += rb.constraints == RigidbodyConstraints2D.FreezePositionY ? 0: dir.y * pushedPower;

                onPushedValueEvent.Invoke(dir.y*pushedPower);
                
                currentPushTween = rb.DOMove(pos , 0.25f);
                onPushedEvent.Invoke();
            }
        }

        public void StopPush()
        {
            if (currentPushTween != null)
            {
                currentPushTween.Kill();
                currentPushTween = null;
            }
        }

        private void OnDestroy()
        {
            StopPush();
        }
    }
}
