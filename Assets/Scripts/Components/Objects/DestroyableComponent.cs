using Components.ExtraComponents;
using UnityEngine;

namespace Components.Objects
{
    public class DestroyableComponent : ComponentBase
    {
        protected override void EnteredCollision(Collision2D other)
        {
            DestroyerComponent destroyerComponentComp = other.gameObject.GetComponent<DestroyerComponent>();
            if (destroyerComponentComp)
            {
                Destroy(gameObject);
            }
        }
    }
}
