using Components.ExtraComponents;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Components.Player_Control
{
    public class ClawMachineControl : BasicMovingControl
    {
        [SerializeField]
        private ClawComponent clawComponent;

        private bool _stoppedMovement;
        protected override void MainActionInputStarted(InputAction.CallbackContext obj)
        {
            clawComponent.MoveClaw();
            animator.SetTrigger("Action");
        }

        protected override void MainActionInputCanceled(InputAction.CallbackContext obj)
        {
            clawComponent.StopClaw();
        }

        protected override void FixedUpdateVirtual()
        {
            if(!_stoppedMovement)
                base.FixedUpdateVirtual();
        }

        public void SetStopMovement(bool stop)
        {
            _stoppedMovement = stop;
        }
    }
}
