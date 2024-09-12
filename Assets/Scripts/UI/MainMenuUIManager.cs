using Core;
using UI.Panel;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace UI
{
    public class MainMenuUIManager : UIManager
    {
        [SerializeField] private PlayerInput mainMenuUIInput;
        
        [Header("Main Menu")]
        [SerializeField] private InputActionReference navigateMenuActionRef; 
        [SerializeField] private InputActionReference quitGameActionRef;

        //cache
        private MainMenuPanel _mainMenuPanel;
        public MainMenuPanel MainMenuPanel => _mainMenuPanel;
        

        public void ToggleInput(bool on)
        {
            if(on)
                mainMenuUIInput.ActivateInput();
            else
                mainMenuUIInput.DeactivateInput();
                
        }

        #region Main Menu Navigation

        public override void InitiatePanel()
        {
            base.InitiatePanel();
            
            _mainMenuPanel = (MainMenuPanel) panelMap["MainMenu"];
            _mainMenuPanel.OnPanelBeginShow.AddListener(MenuPanelStartShown);
            _mainMenuPanel.OnPanelFinishShow.AddListener(MenuPanelFinishShown);
            _mainMenuPanel.ShowPanel();

            mainMenuUIInput.onControlsChanged += ControlsChanged;
        }
        
        public void ControlsChanged(PlayerInput obj)
        {
            Debug.Log("Current control is "+ obj.currentControlScheme);
        }

        private void MenuPanelStartShown()
        {
            _mainMenuPanel.OnPanelBeginShow.RemoveListener(MenuPanelStartShown);
            ToggleInput(false);
        }

        private void MenuPanelFinishShown()
        {
            _mainMenuPanel.OnPanelFinishShow.RemoveListener(MenuPanelFinishShown);
            BindInputMainMenu();
            mainMenuUIInput.SwitchCurrentActionMap("MainMenu");
            ToggleInput(true);
            _mainMenuPanel.thisButtonParent.NavigateButton(false);
        }

        private void BindInputMainMenu()
        {
            navigateMenuActionRef.action.started += OnMenuNavigated;
            quitGameActionRef.action.started += OnQuitPressed;
        }
        
        private void OnMenuNavigated(InputAction.CallbackContext obj)
        {
            float value = obj.ReadValue<float>();

            if (value > 0)
            {
                _mainMenuPanel.thisButtonParent.NavigateButton(true);
            }
            else
            {
                _mainMenuPanel.thisButtonParent.NavigateButton(false);
            }
        }

        private void OnQuitPressed(InputAction.CallbackContext obj)
        {
            QuitPopUp();
        }

        #endregion

        #region Public Inspector Functions
        
        public void PlayGame()
        {
            ToggleInput(false);
            FindObjectOfType<EventSystem>().SetSelectedGameObject(null);
            GameManager.instance.PlayGame();
        }
        
        public void QuitPopUp()
        {
            //Show quit confirmation popup
            Application.Quit();
        }

        #endregion

    }
}