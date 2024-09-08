using Components.Objects;
using Components.Player_Control_Components;
using UnityEngine;

namespace Components.ExtraComponents
{
    public class PlatformFloor : ComponentBase
    {
        protected override void EnteredCollision(Collision2D other)
        {
            if (other.gameObject.GetComponent<PlayerControl>())
            {
                transform.parent.GetComponent<PlatformComponent>().onPlatformStepped.Invoke();
            }
        }

        protected override void ExitCollision(Collision2D other)
        {
            if (other.gameObject.GetComponent<PlayerControl>())
            {
                transform.parent.GetComponent<PlatformComponent>().onPlatformLeft.Invoke();
            }
        }
    }
}