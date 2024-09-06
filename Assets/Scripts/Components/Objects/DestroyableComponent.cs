using Components.ExtraComponents;
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
                Destroy(gameObject);
            }
        }
    }
}
