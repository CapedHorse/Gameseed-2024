using Components.ExtraComponents;
using Components.Player_Control_Components;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Components.Objects
{
    public class GrabableComponent : ComponentBase
    {
        protected bool _grabbed;

        protected override void EnteredTrigger(Collider2D other)
        {
            ClawComponent claw = other.GetComponent<ClawComponent>();
            if (claw != null)
            {
                _grabbed = true;
                transform.SetParent(claw.transform);
            }
        }
    }
}
