using Components.Objects;
using UnityEngine;

namespace Components.ExtraComponents
{
    public class VineDetector : ComponentBase
    {
        [SerializeField] private ClawComponent claw;

        protected override void EnteredTrigger(Collider2D other)
        {
            if (other.GetComponent<ClawBlocker>())
            {
                claw.ForceRetract();
            }
        }
    }
}