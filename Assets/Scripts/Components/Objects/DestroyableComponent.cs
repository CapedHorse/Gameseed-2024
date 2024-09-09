using Components.ExtraComponents;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects
{
    public class DestroyableComponent : ComponentBase
    {
        public UnityEvent onDestroyedEvent;
        protected override void EnteredCollision(Collision2D other)
        {
            DestroyerComponent destroyerComponentComp = other.gameObject.GetComponent<DestroyerComponent>();
            
            if (destroyerComponentComp)
            {
                onDestroyedEvent.Invoke();
                transform.DOShakeRotation(0.25f, Vector3.one * 10, 150);
                Destroy(gameObject, 0.25f);
            }
        }
    }
}
