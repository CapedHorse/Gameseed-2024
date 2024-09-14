using UnityEngine;
using UnityEngine.InputSystem;

namespace Components.Player_Control
{
    public class PlayerControl : MonoBehaviour
    {
        [SerializeField] protected PlayerInput playerInput;
        [SerializeField] private InputActionReference movingInputRef;
        [SerializeField] private InputActionReference mainActionInputRef;
        
        public void ToggleInput(bool enabled)
        {
            if(enabled)
                playerInput.ActivateInput();
            else
                playerInput.DeactivateInput();
        }
        
        private void OnEnable()
        {
            movingInputRef.action.started += MovingInputStarted;
            movingInputRef.action.performed += MovingInputPerformed;
            movingInputRef.action.canceled += MovingInputCanceled;
            
            mainActionInputRef.action.started += MainActionInputStarted;
            mainActionInputRef.action.performed += MainActionInputPerformed;
            mainActionInputRef.action.canceled += MainActionInputCanceled;

            ToggleInput(false);
        }

        protected virtual void MovingInputStarted(InputAction.CallbackContext obj)
        {
            
        }

        protected virtual void MovingInputPerformed(InputAction.CallbackContext obj)
        {
            
        }

        protected virtual void MovingInputCanceled(InputAction.CallbackContext obj)
        {
            
        }
        
        protected virtual void MainActionInputStarted(InputAction.CallbackContext obj)
        {
            
        }

        protected virtual void MainActionInputPerformed(InputAction.CallbackContext obj)
        {
            
        }

        protected virtual void MainActionInputCanceled(InputAction.CallbackContext obj)
        {
            
        }

        private void OnDisable()
        {
            movingInputRef.action.started -= MovingInputStarted;
            movingInputRef.action.performed -= MovingInputPerformed;
            movingInputRef.action.canceled -= MovingInputCanceled;
            
            mainActionInputRef.action.started -= MainActionInputStarted;
            mainActionInputRef.action.performed -= MainActionInputPerformed;
            mainActionInputRef.action.canceled -= MainActionInputCanceled;
        }

        public void EnableInput()
        {
            playerInput.ActivateInput();
        }

        public void DisableInput()
        {
            playerInput.DeactivateInput();
        }
    }
}
