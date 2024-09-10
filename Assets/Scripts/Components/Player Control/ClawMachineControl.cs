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
            clawComponent.MoveClaw();
        }

        protected override void MainActionInputCanceled(InputAction.CallbackContext obj)
        {
            clawComponent.StopClaw();
        }
    }
}
