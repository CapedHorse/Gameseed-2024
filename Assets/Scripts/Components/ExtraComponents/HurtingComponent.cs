using Components.Objects;
using Components.Player_Control_Components;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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