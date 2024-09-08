using Components.Objects;
using UnityEngine;

namespace Components.ExtraComponents
{
    public class HurtingComponent : ComponentBase
    {
        
        protected override void EnteredTrigger(Collider2D other)
        {
            
            HurtComponent hurtComponent = other.GetComponent<HurtComponent>();
            if (hurtComponent != null)
            {
                hurtComponent.GotHurt();
            }
        }
    }
}