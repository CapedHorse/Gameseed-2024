using Components.ExtraComponents;
using UnityEngine;
using UnityEngine.Events;

namespace Components.Objects
{
    public class GrabableComponent : ComponentBase
    {
        protected bool _grabbed;

        public UnityEvent onGrabbedEvent;
        protected override void EnteredTrigger(Collider2D other)
        {
            ClawComponent claw = other.GetComponent<ClawComponent>();
            if (claw != null)
            {
                _grabbed = true;
                transform.SetParent(claw.transform);
                onGrabbedEvent.Invoke();
            }
        }
    }
}
