using Components.ExtraComponents;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Components.Player_Control
{
    public class ClawMachineControl : BasicMovingControl
    {
        [SerializeField]
        private ClawComponent clawComponent;

        protected override void MainActionInputStarted(InputAction.CallbackContext obj)
        {
            clawComponent.LaunchClaw();
        }
    }
}
