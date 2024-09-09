using Components.ExtraComponents;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects.SpecificObjects.Pistachio
{
    public class BulletPushable : ComponentBase
    {
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private float pushedPower = 50f;
        public UnityEvent onPushedEvent;
        public UnityEvent<float> onPushedValueEvent;

        private Tweener currentPushTween;
        protected override void EnteredCollision(Collision2D other)
        {
            if (other.gameObject.GetComponent<BulletComponent>())
            {
                if(currentPushTween != null)
                    currentPushTween.Kill();
                
                Vector2 dir = other.GetContact(0).normal;
                Vector2 pos = transform.position;
                Debug.Log("Actual position "+pos);
                pos.x += rb.constraints == RigidbodyConstraints2D.FreezePositionX ? 0 : dir.x * pushedPower;
                pos.y += rb.constraints == RigidbodyConstraints2D.FreezePositionY ? 0: dir.y * pushedPower;

                onPushedValueEvent.Invoke(dir.y*pushedPower);
                Debug.Log("Destination "+pos);
                currentPushTween = rb.DOMove(pos , 0.25f);
                onPushedEvent.Invoke();
            }
        }
    }
}